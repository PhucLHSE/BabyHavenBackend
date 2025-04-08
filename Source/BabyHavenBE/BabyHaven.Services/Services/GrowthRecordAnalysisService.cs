using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.AIChatDTOs;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.Extensions.Configuration;

namespace BabyHaven.Services.Services
{
    public class GrowthRecordAnalysisService : IGrowthAnalysisService
    {
        private readonly IConfiguration _config;
        private static readonly Dictionary<string, List<ChatMessage>> ChatHistories = new Dictionary<string, List<ChatMessage>>();
        private static readonly object _lock = new object();

        public GrowthRecordAnalysisService(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<IServiceResult> AnalyzeGrowthRecord(GrowthRecordAnalysisDto record)
        {
            var apiKey = _config["Gemini:ApiKey"];
            var endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            if (string.IsNullOrEmpty(apiKey))
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, "Missing Gemini API Key");
            }

            var prompt = $"Analyze this child's growth data:\n" +
                         $"Weight: {record.Weight} kg\n" +
                         $"Height: {record.Height} cm\n" +
                         $"Chest Circumference: {record.ChestCircumference} cm\n" +
                         $"BMI: {record.BMI}\n" +
                         $"Nutritional Status: {record.NutritionalStatus}\n" +
                         "Please provide an age-specific health analysis and recommendations.";

            var messages = new List<ChatMessage>
            {
                new ChatMessage("user", "You are an AI assistant that provides health analysis based on growth data. " + prompt)
            };

            var requestBody = new
            {
                contents = messages.Select(m => new
                {
                    role = m.Role,
                    parts = new[] { new { text = m.Content } }
                }).ToList(),
                generationConfig = new
                {
                    temperature = 1.0,
                    topP = 0.95,
                    topK = 40,
                    maxOutputTokens = 8192,
                    responseMimeType = "text/plain"
                }
            };

            using var client = new HttpClient();
            int maxRetries = 3;
            int retryDelaySeconds = 60;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                HttpResponseMessage response = null;
                try
                {
                    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                    response = await client.PostAsJsonAsync(endpoint, requestBody, cts.Token);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                        var analysisResult = result.GetProperty("candidates")[0]
                                                  .GetProperty("content")
                                                  .GetProperty("parts")[0]
                                                  .GetProperty("text")
                                                  .GetString();

                        if (string.IsNullOrEmpty(analysisResult))
                        {
                            analysisResult = "No response received.";
                        }

                        return new ServiceResult(Const.SUCCESS_READ_CODE, "Growth record analysis completed.", new { analysisResult });
                    }
                    else if ((int)response.StatusCode == 429 && attempt < maxRetries)
                    {
                        Console.WriteLine($"[AnalyzeGrowthRecord] Rate limit exceeded. Retrying after {retryDelaySeconds} seconds (Attempt {attempt}/{maxRetries})...");
                        await Task.Delay(retryDelaySeconds * 1000);
                        continue;
                    }
                    else if ((int)response.StatusCode == 400)
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        return new ServiceResult(Const.ERROR_EXCEPTION, $"Error in AI analysis: Bad Request - {errorResponse}");
                    }
                    else
                    {
                        return new ServiceResult(Const.ERROR_EXCEPTION, $"Error in AI analysis: Response status code {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return new ServiceResult(Const.ERROR_EXCEPTION, $"Error in AI analysis: {ex.Message}");
                }
                finally
                {
                    response?.Dispose();
                }
            }

            return new ServiceResult(Const.ERROR_EXCEPTION, "Failed to get response from AI after multiple retries due to rate limit.");
        }

        public async Task<IServiceResult> ChatWithAI(string sessionId, string userMessage, GrowthRecordAnalysisDto initialRecord = null)
        {
            var apiKey = _config["Gemini:ApiKey"];
            var endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            if (string.IsNullOrEmpty(apiKey))
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, "Missing Gemini API Key");
            }

            // Khởi tạo lịch sử trò chuyện nếu chưa tồn tại
            lock (_lock)
            {
                if (!ChatHistories.ContainsKey(sessionId))
                {
                    ChatHistories[sessionId] = new List<ChatMessage>();

                    var systemPrompt = $"You are a pediatric health consultant for children aged {initialRecord.Age} named {initialRecord.ChildName}. Provide evidence-based advice on growth, health, and development. Analyze growth data, offer age-specific recommendations, and address concerns. Use a clear, empathetic tone. Do not diagnose or prescribe medication.";

                    if (initialRecord != null)
                    {
                        var initialPrompt = $"Here is the child's growth data:\n" +
                                           $"Age: {initialRecord.Age} years\n" +
                                           $"Weight: {initialRecord.Weight} kg\n" +
                                           $"Height: {initialRecord.Height} cm\n" +
                                           $"Chest Circumference: {initialRecord.ChestCircumference} cm\n" +
                                           $"Muscle Mass: {initialRecord.MuscleMass} kg\n" +
                                           $"Blood Sugar Level: {initialRecord.BloodSugarLevel} mg/dL\n" +
                                           $"Triglycerides: {initialRecord.Triglycerides} mg/dL\n" +
                                           $"Nutritional Status: {initialRecord.NutritionalStatus}\n" +
                                           "I will ask questions or request advice based on this data.";
                        ChatHistories[sessionId].Add(new ChatMessage("user", systemPrompt + "\n\n" + initialPrompt));
                    }
                    else
                    {
                        ChatHistories[sessionId].Add(new ChatMessage("user", systemPrompt));
                    }
                }

                ChatHistories[sessionId].Add(new ChatMessage("user", userMessage));
            }

            var messagesToSend = ChatHistories[sessionId]
                .TakeLast(3)
                .Where(m => !string.IsNullOrWhiteSpace(m.Content)) 
                .Select(m => new
                {
                    role = m.Role == "assistant" ? "model" : m.Role,
                    parts = new[] { new { text = m.Content } }
                })
                .ToList();

            var requestBody = new
            {
                contents = messagesToSend,
                generationConfig = new
                {
                    temperature = 1.0,
                    topP = 0.95,
                    topK = 40,
                    maxOutputTokens = 8192,
                    responseMimeType = "text/plain"
                }
            };

            using var client = new HttpClient();
            int maxRetries = 3;
            int retryDelaySeconds = 60;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                HttpResponseMessage response = null;
                try
                {
                    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                    response = await client.PostAsJsonAsync(endpoint, requestBody, cts.Token);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                        var aiResponse = result.GetProperty("candidates")[0]
                                              .GetProperty("content")
                                              .GetProperty("parts")[0]
                                              .GetProperty("text")
                                              .GetString();

                        if (string.IsNullOrEmpty(aiResponse))
                        {
                            aiResponse = "No response received.";
                        }

                        lock (_lock)
                        {
                            ChatHistories[sessionId].Add(new ChatMessage("model", aiResponse));
                        }

                        return new ServiceResult(Const.SUCCESS_READ_CODE, "AI response generated.", new { aiResponse });
                    }
                    else if ((int)response.StatusCode == 429 && attempt < maxRetries)
                    {
                        Console.WriteLine($"[ChatWithAI] Rate limit exceeded. Retrying after {retryDelaySeconds} seconds (Attempt {attempt}/{maxRetries})...");
                        await Task.Delay(retryDelaySeconds * 1000);
                        continue;
                    }
                    else if ((int)response.StatusCode == 400)
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        return new ServiceResult(Const.ERROR_EXCEPTION, $"Error in AI chat: Bad Request - {errorResponse}");
                    }
                    else
                    {
                        return new ServiceResult(Const.ERROR_EXCEPTION, $"Error in AI chat: Response status code {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return new ServiceResult(Const.ERROR_EXCEPTION, $"Error in AI chat: {ex.Message}");
                }
                finally
                {
                    response?.Dispose();
                }
            }

            return new ServiceResult(Const.ERROR_EXCEPTION, "Failed to get response from AI after multiple retries due to rate limit.");
        }

        public void ClearChatHistory(string sessionId)
        {
            lock (_lock)
            {
                if (ChatHistories.ContainsKey(sessionId))
                {
                    ChatHistories.Remove(sessionId);
                }
            }
        }
    }
}