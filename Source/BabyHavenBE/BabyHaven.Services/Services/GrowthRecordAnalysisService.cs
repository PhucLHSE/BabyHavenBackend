﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.GrowthRecordDTOs;
using BabyHaven.Common;
using BabyHaven.Repositories.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using Azure;
using Microsoft.Extensions.Configuration;

namespace BabyHaven.Services.Services
{
    public class GrowthRecordAnalysisService : IGrowthAnalysisService
    {
        private readonly IConfiguration _config;

        public GrowthRecordAnalysisService(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<IServiceResult> AnalyzeGrowthRecord(GrowthRecordAnalysisDto record)
        {
            var endpoint = _config["AzureOpenAI:Endpoint"];
            var apiKey = _config["AzureOpenAI:ApiKey"];
            var deploymentName = _config["AzureOpenAI:DeploymentName"];

            if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(apiKey))
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, "Missing API Key or Endpoint");
            }

            var azureClient = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
            ChatClient chatClient = azureClient.GetChatClient(deploymentName);

            var prompt = $"Analyze this child's growth data:\n" +
                         $"Weight: {record.Weight} kg\n" +
                         $"Height: {record.Height} cm\n" +
                         $"Chest Circumference: {record.ChestCircumference} cm\n" +
                         $"BMI: {record.BMI}\n" +
                         $"Nutritional Status: {record.NutritionalStatus}\n" +
                         "Please provide an age-specific health analysis and recommendations.";

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are an AI assistant that provides health analysis based on growth data."),
                new UserChatMessage(prompt)
            };

            var options = new ChatCompletionOptions
            {
                Temperature = 0.7f,
                MaxOutputTokenCount = 800,
                FrequencyPenalty = 0,
                PresencePenalty = 0
            };

            try
            {
                ChatCompletion response = await chatClient.CompleteChatAsync(messages, options);
                var analysisResult = response.Content != null && response.Content.Count > 0
                    ? response.Content[0].Text.Trim()
                    : "No response received.";

                return new ServiceResult(Const.SUCCESS_READ_CODE, "Growth record analysis completed.", new { analysisResult });
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"Error in AI analysis: {ex.Message}");
            }
        }
    }
}
