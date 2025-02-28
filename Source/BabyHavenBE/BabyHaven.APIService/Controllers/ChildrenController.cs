using BabyHaven.Common;
using BabyHaven.Common.DTOs.ChildrenDTOs;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildrenController : ControllerBase
    {
        private readonly IChildrenService _childrenService;

        public ChildrenController(IChildrenService childrenService)
        {
            _childrenService = childrenService ?? throw new ArgumentNullException(nameof(childrenService));
        }

        /// <summary>
        /// Creates a new child.
        /// </summary>
        /// <param name="dto">The child creation DTO.</param>
        /// <returns>The result of the creation operation.</returns>
        [HttpPost]
        public async Task<IServiceResult> CreateChild([FromBody] ChildCreateDto dto)
        {
            if (!ModelState.IsValid)
                return new ServiceResult { Status = Const.ERROR_VALIDATION_CODE, Message = "Invalid model state." };

            return await _childrenService.CreateChild(dto);
        }
        /// <summary>
        /// Creates a new child anytime.
        /// </summary>
        /// <param name="dto">The child creation DTO.</param>
        /// <returns>The result of the creation operation.</returns>
        [HttpPost("/ChildForNow")]
        public async Task<IServiceResult> CreateChildForNow([FromBody] ChildCreateForNowDto dto)
        {
            if (!ModelState.IsValid)
                return new ServiceResult { Status = Const.ERROR_VALIDATION_CODE, Message = "Invalid model state." };

            return await _childrenService.CreateChildForNow(dto);
        }

        /// <summary>
        /// Deletes a child by ID.
        /// </summary>
        /// <param name="childId">The ID of the child to delete.</param>
        /// <returns>The result of the deletion operation.</returns>
        [HttpDelete("{childId}")]
        public async Task<IServiceResult> DeleteChildById(Guid childId)
        {
            return await _childrenService.DeleteChildById(childId);
        }

        /// <summary>
        /// Gets all children.
        /// </summary>
        /// <returns>The result of the retrieval operation.</returns>
        [HttpGet]
        public async Task<IServiceResult> GetAllChildren()
        {
            return await _childrenService.GetAllChildren();
        }

        /// <summary>
        /// Gets a child by ID.
        /// </summary>
        /// <param name="childId">The ID of the child to retrieve.</param>
        /// <returns>The result of the retrieval operation.</returns>
        [HttpGet("{childId}")]
        public async Task<IServiceResult> GetChildById(Guid childId)
        {
            return await _childrenService.GetChildById(childId);
        }

        /// <summary>
        /// Gets children by member ID.
        /// </summary>
        /// <param name="memberId">The ID of the member whose children to retrieve.</param>
        /// <returns>The result of the retrieval operation.</returns>
        [HttpGet("member/{memberId}")]
        public async Task<IServiceResult> GetChildrenByMemberId(Guid memberId)
        {
            return await _childrenService.GetChildrenByMemberId(memberId);
        }

        /// <summary>
        /// Marks a child for deletion by ID.
        /// </summary>
        /// <param name="childId">The ID of the child to mark for deletion.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPut("pre-delete/{childId}")]
        public async Task<IServiceResult> PreDeleteById(Guid childId)
        {
            return await _childrenService.PreDeleteById(childId);
        }

        /// <summary>
        /// Recovers a child by ID.
        /// </summary>
        /// <param name="childId">The ID of the child to recover.</param>
        /// <returns>The result of the recovery operation.</returns>
        [HttpPut("recover/{childId}")]
        public async Task<IServiceResult> RecoverById(Guid childId)
        {
            return await _childrenService.RecoverById(childId);
        }

        /// <summary>
        /// Updates a child by ID.
        /// </summary>
        /// <param name="dto">The child update DTO.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut]
        public async Task<IServiceResult> UpdateChildById([FromBody] ChildUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return new ServiceResult { Status = Const.ERROR_VALIDATION_CODE, Message = "Invalid model state." };

            return await _childrenService.UpdateChildById(dto);
        }
    }
}
