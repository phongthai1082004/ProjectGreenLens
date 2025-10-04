using Microsoft.AspNetCore.Mvc;
using ProjectGreenLens.Models.DTOs.Plant;
using ProjectGreenLens.Services.Interfaces;
using ProjectGreenLens.Settings;

namespace ProjectGreenLens.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantsController : ControllerBase
    {
        private readonly IPlantService _plantService;

        public PlantsController(IPlantService plantService)
        {
            _plantService = plantService;
        }

        #region Main Query Endpoints

        // GET: api/plants
        /// <summary>
        /// Lấy danh sách cây với filter, search, sort và pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetPlants([FromQuery] PlantQueryDto query)
        {
            var result = await _plantService.GetPlantsAsync(query);
            var message = BuildQueryMessage(query, result.TotalCount);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result, message));
        }

        #endregion

        #region Convenient Query Endpoints

        // GET: api/plants/indoor
        /// <summary>
        /// Lấy danh sách cây trong nhà
        /// </summary>
        [HttpGet("indoor")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetIndoorPlants(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new PlantQueryDto
            {
                IsIndoor = true,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = PlantSortBy.ScientificName,
                SortDirection = SortDirection.Ascending
            };

            var result = await _plantService.GetPlantsAsync(query);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                $"Lấy danh sách cây trong nhà thành công. Trang {pageNumber}, tìm thấy {result.TotalCount} kết quả"));
        }

        // GET: api/plants/outdoor
        /// <summary>
        /// Lấy danh sách cây ngoài trời
        /// </summary>
        [HttpGet("outdoor")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetOutdoorPlants(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new PlantQueryDto
            {
                IsIndoor = false,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = PlantSortBy.ScientificName,
                SortDirection = SortDirection.Ascending
            };

            var result = await _plantService.GetPlantsAsync(query);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                $"Lấy danh sách cây ngoài trời thành công. Trang {pageNumber}, tìm thấy {result.TotalCount} kết quả"));
        }

        // GET: api/plants/search?term=rosa
        /// <summary>
        /// Tìm kiếm cây theo từ khóa
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> SearchPlants(
            [FromQuery] string term,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return BadRequest(ApiResponse<PagedResult<PlantResponseDto>>.Fail("Từ khóa tìm kiếm không được để trống"));
            }

            var query = new PlantQueryDto
            {
                SearchTerm = term,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = PlantSortBy.ScientificName,
                SortDirection = SortDirection.Ascending
            };

            var result = await _plantService.GetPlantsAsync(query);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                $"Tìm kiếm '{term}' thành công. Trang {pageNumber}, tìm thấy {result.TotalCount} kết quả"));
        }

        // GET: api/plants/category/{categoryId}
        /// <summary>
        /// Lấy cây theo danh mục
        /// </summary>
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetPlantsByCategory(
            int categoryId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool includeDetails = false)
        {
            var query = new PlantQueryDto
            {
                PlantCategoryId = categoryId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                IncludeDetails = includeDetails,
                SortBy = PlantSortBy.ScientificName,
                SortDirection = SortDirection.Ascending
            };

            var result = await _plantService.GetPlantsAsync(query);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                $"Lấy cây danh mục {categoryId} thành công. Trang {pageNumber}, tìm thấy {result.TotalCount} kết quả"));
        }

        // GET: api/plants/care-requirements
        /// <summary>
        /// Lấy cây theo yêu cầu chăm sóc
        /// </summary>
        [HttpGet("care-requirements")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetPlantsByCareRequirements(
            [FromQuery] int? wateringFrequency = null,
            [FromQuery] int? lightRequirement = null,
            [FromQuery] bool? isIndoor = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new PlantQueryDto
            {
                WateringFrequency = wateringFrequency,
                IsIndoor = isIndoor,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = PlantSortBy.WateringFrequency,
                SortDirection = SortDirection.Ascending
            };

            var result = await _plantService.GetPlantsAsync(query);
            var careDescription = BuildCareRequirementsDescription(wateringFrequency, lightRequirement, isIndoor);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                $"Lấy cây theo yêu cầu chăm sóc {careDescription} thành công. Tìm thấy {result.TotalCount} kết quả"));
        }

        // GET: api/plants/affordable?maxPrice=100000
        /// <summary>
        /// Lấy cây giá rẻ dưới mức giá nhất định
        /// </summary>
        [HttpGet("affordable")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetAffordablePlants(
            [FromQuery] decimal maxPrice,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new PlantQueryDto
            {
                MaxPrice = maxPrice,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = PlantSortBy.AveragePrice,
                SortDirection = SortDirection.Ascending
            };

            var result = await _plantService.GetPlantsAsync(query);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                $"Lấy cây giá dưới {maxPrice:N0} VND thành công. Tìm thấy {result.TotalCount} kết quả"));
        }

        // GET: api/plants/premium?minPrice=500000
        /// <summary>
        /// Lấy cây cao cấp trên mức giá nhất định
        /// </summary>
        [HttpGet("premium")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetPremiumPlants(
            [FromQuery] decimal minPrice,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new PlantQueryDto
            {
                MinPrice = minPrice,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = PlantSortBy.AveragePrice,
                SortDirection = SortDirection.Descending
            };

            var result = await _plantService.GetPlantsAsync(query);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                $"Lấy cây cao cấp trên {minPrice:N0} VND thành công. Tìm thấy {result.TotalCount} kết quả"));
        }

        #endregion

        #region Predefined Queries

        // GET: api/plants/popular-indoor
        /// <summary>
        /// Lấy cây trong nhà phổ biến
        /// </summary>
        [HttpGet("popular-indoor")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetPopularIndoorPlants(
            [FromQuery] int pageSize = 10)
        {
            var query = new PlantQueryDto
            {
                IsIndoor = true,
                PageSize = pageSize,
                PageNumber = 1,
                SortBy = PlantSortBy.CommonName,
                SortDirection = SortDirection.Ascending,
                IncludeDetails = false
            };

            var result = await _plantService.GetPlantsAsync(query);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                $"Lấy {pageSize} cây trong nhà phổ biến thành công"));
        }

        // GET: api/plants/budget-friendly
        /// <summary>
        /// Lấy cây giá rẻ (dưới 100k)
        /// </summary>
        [HttpGet("budget-friendly")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetBudgetFriendlyPlants(
            [FromQuery] int pageSize = 20)
        {
            var query = new PlantQueryDto
            {
                MaxPrice = 100000,
                PageSize = pageSize,
                PageNumber = 1,
                SortBy = PlantSortBy.AveragePrice,
                SortDirection = SortDirection.Ascending,
                IncludeDetails = false
            };

            var result = await _plantService.GetPlantsAsync(query);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                "Lấy cây giá rẻ (dưới 100,000 VND) thành công"));
        }

        // GET: api/plants/easy-care
        /// <summary>
        /// Lấy cây dễ chăm sóc
        /// </summary>
        [HttpGet("easy-care")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetEasyCarePlants(
            [FromQuery] int pageSize = 15)
        {
            var query = new PlantQueryDto
            {
                WateringFrequency = 7, // Weekly watering
                PageSize = pageSize,
                PageNumber = 1,
                SortBy = PlantSortBy.ScientificName,
                SortDirection = SortDirection.Ascending,
                IncludeDetails = false
            };

            var result = await _plantService.GetPlantsAsync(query);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                "Lấy cây dễ chăm sóc thành công"));
        }

        // GET: api/plants/newest
        /// <summary>
        /// Lấy cây mới nhất
        /// </summary>
        [HttpGet("newest")]
        public async Task<ActionResult<ApiResponse<PagedResult<PlantResponseDto>>>> GetNewestPlants(
            [FromQuery] int pageSize = 10)
        {
            var query = new PlantQueryDto
            {
                PageSize = pageSize,
                PageNumber = 1,
                SortBy = PlantSortBy.CreatedAt,
                SortDirection = SortDirection.Descending,
                IncludeDetails = true
            };

            var result = await _plantService.GetPlantsAsync(query);
            return Ok(ApiResponse<PagedResult<PlantResponseDto>>.Ok(result,
                "Lấy cây mới nhất thành công"));
        }

        #endregion

        #region Single Item Endpoints

        // GET: api/plants/{id}
        /// <summary>
        /// Lấy thông tin cây theo ID (không bao gồm navigation properties)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PlantResponseDto>>> GetById(int id)
        {
            var result = await _plantService.GetByIdAsync(id);
            return Ok(ApiResponse<PlantResponseDto>.Ok(result, "Lấy thông tin cây thành công"));
        }

        // GET: api/plants/{id}/details
        /// <summary>
        /// Lấy thông tin chi tiết của một cây theo ID (bao gồm navigation properties)
        /// </summary>
        [HttpGet("{id}/details")]
        public async Task<ActionResult<ApiResponse<PlantResponseDto>>> GetByIdWithDetails(int id)
        {
            var result = await _plantService.GetByIdWithDetailsAsync(id);
            return Ok(ApiResponse<PlantResponseDto>.Ok(result, "Lấy thông tin chi tiết cây thành công"));
        }

        #endregion

        #region CRUD Operations

        // POST: api/plants
        /// <summary>
        /// Tạo mới cây
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PlantResponseDto>>> Create([FromBody] PlantAddDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<PlantResponseDto>.Fail("Dữ liệu không hợp lệ", errors));
            }

            var result = await _plantService.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                ApiResponse<PlantResponseDto>.Ok(result, "Tạo cây mới thành công")
            );
        }

        // PUT: api/plants
        /// <summary>
        /// Cập nhật thông tin cây
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<ApiResponse<PlantResponseDto>>> Update([FromBody] PlantUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<PlantResponseDto>.Fail("Dữ liệu không hợp lệ", errors));
            }

            var result = await _plantService.UpdateAsync(dto);
            return Ok(ApiResponse<PlantResponseDto>.Ok(result, "Cập nhật cây thành công"));
        }

        // DELETE: api/plants/{id}
        /// <summary>
        /// Xóa cây (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var result = await _plantService.DeleteAsync(id);
            return Ok(ApiResponse<bool>.Ok(result, "Xóa cây thành công"));
        }

        #endregion

        #region Statistics

        // GET: api/plants/stats
        /// <summary>
        /// Lấy thống kê về cây
        /// </summary>
        [HttpGet("stats")]
        public async Task<ActionResult<ApiResponse<object>>> GetPlantStats()
        {
            var allPlantsQuery = new PlantQueryDto
            {
                PageSize = int.MaxValue,
                PageNumber = 1,
                IncludeDetails = false
            };

            var allPlants = await _plantService.GetPlantsAsync(allPlantsQuery);
            var plantsList = allPlants.Items.ToList();

            var stats = new
            {
                TotalPlants = allPlants.TotalCount,
                IndoorPlants = plantsList.Count(p => p.IsIndoor),
                OutdoorPlants = plantsList.Count(p => !p.IsIndoor),
                PlantsWithCategory = plantsList.Count(p => p.PlantCategoryId.HasValue),
                PlantsWithoutCategory = plantsList.Count(p => !p.PlantCategoryId.HasValue),
                AveragePrice = plantsList.Where(p => p.AveragePrice.HasValue).Any() ?
                              plantsList.Where(p => p.AveragePrice.HasValue).Average(p => p.AveragePrice.Value) : 0,
                PriceRange = new
                {
                    MinPrice = plantsList.Where(p => p.AveragePrice.HasValue).Any() ?
                              plantsList.Where(p => p.AveragePrice.HasValue).Min(p => p.AveragePrice.Value) : 0,
                    MaxPrice = plantsList.Where(p => p.AveragePrice.HasValue).Any() ?
                              plantsList.Where(p => p.AveragePrice.HasValue).Max(p => p.AveragePrice.Value) : 0
                },
                WateringFrequencyDistribution = plantsList
                    .GroupBy(p => p.WateringFrequency)
                    .Select(g => new { Frequency = g.Key, Count = g.Count() })
                    .OrderBy(x => x.Frequency)
                    .ToList(),
                LightRequirementDistribution = plantsList
                    .GroupBy(p => p.LightRequirement)
                    .Select(g => new { Requirement = g.Key, Count = g.Count() })
                    .OrderBy(x => x.Requirement)
                    .ToList(),
                CategoryDistribution = plantsList
                    .Where(p => !string.IsNullOrEmpty(p.PlantCategoryName))
                    .GroupBy(p => p.PlantCategoryName)
                    .Select(g => new { CategoryName = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList()
            };

            return Ok(ApiResponse<object>.Ok(stats, "Lấy thống kê cây thành công"));
        }

        #endregion

        #region Private Helper Methods

        private string BuildQueryMessage(PlantQueryDto query, int totalCount)
        {
            var filters = new List<string>();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
                filters.Add($"từ khóa '{query.SearchTerm}'");

            if (query.PlantCategoryId.HasValue)
                filters.Add($"danh mục {query.PlantCategoryId.Value}");

            if (query.IsIndoor.HasValue)
                filters.Add(query.IsIndoor.Value ? "trong nhà" : "ngoài trời");

            if (query.WateringFrequency.HasValue)
                filters.Add($"tần suất tưới {query.WateringFrequency.Value} ngày");

            if (!string.IsNullOrWhiteSpace(query.SoilType))
                filters.Add($"loại đất '{query.SoilType}'");

            if (query.MinPrice.HasValue || query.MaxPrice.HasValue)
            {
                if (query.MinPrice.HasValue && query.MaxPrice.HasValue)
                    filters.Add($"giá từ {query.MinPrice.Value:N0} đến {query.MaxPrice.Value:N0} VND");
                else if (query.MinPrice.HasValue)
                    filters.Add($"giá từ {query.MinPrice.Value:N0} VND");
                else
                    filters.Add($"giá đến {query.MaxPrice.Value:N0} VND");
            }

            var filterText = filters.Any() ? $" với {string.Join(", ", filters)}" : "";
            var sortText = GetSortText(query.SortBy, query.SortDirection);

            return $"Lấy danh sách cây{filterText}, sắp xếp theo {sortText} thành công. " +
                   $"Tìm thấy {totalCount} kết quả, trang {query.PageNumber}/{Math.Ceiling((double)totalCount / query.PageSize)}";
        }

        private string BuildCareRequirementsDescription(int? wateringFreq, int? lightReq, bool? isIndoor)
        {
            var parts = new List<string>();

            if (wateringFreq.HasValue)
                parts.Add($"tưới {wateringFreq.Value} ngày/lần");

            if (lightReq.HasValue)
                parts.Add($"ánh sáng mức {lightReq.Value}");

            if (isIndoor.HasValue)
                parts.Add(isIndoor.Value ? "trong nhà" : "ngoài trời");

            return parts.Any() ? string.Join(", ", parts) : "không yêu cầu cụ thể";
        }

        private string GetSortText(PlantSortBy sortBy, SortDirection sortDirection)
        {
            var sortName = sortBy switch
            {
                PlantSortBy.Id => "ID",
                PlantSortBy.ScientificName => "tên khoa học",
                PlantSortBy.CommonName => "tên thông thường",
                PlantSortBy.CreatedAt => "ngày tạo",
                PlantSortBy.AveragePrice => "giá trung bình",
                PlantSortBy.WateringFrequency => "tần suất tưới",
                PlantSortBy.LightRequirement => "yêu cầu ánh sáng",
                PlantSortBy.CategoryName => "tên danh mục",
                _ => "ngày tạo"
            };

            var direction = sortDirection == SortDirection.Ascending ? "tăng dần" : "giảm dần";
            return $"{sortName} ({direction})";
        }

        #endregion
    }
}