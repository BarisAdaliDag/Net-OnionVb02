//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.CategoryCommands;
//using OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify;
//using OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read;
//using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.CategoryQueries;
//using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.CategoryResults;

//namespace OnionVb02.WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoryCQRSController : ControllerBase
//    {
//        private readonly GetCategoryQueryHandler _getCategoryQueryHandler;
//        private readonly GetCategoryByIdQueryHandler _getCategoryByIdQueryHandler;
//        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler;
//        private readonly UpdateCategoryCommandHandler _updateCategoryCommandHandler;
//        private readonly RemoveCategoryCommandHandler _removeCategoryCommandHandler;

//        public CategoryCQRSController(GetCategoryQueryHandler getCategoryQueryHandler, GetCategoryByIdQueryHandler getCategoryByIdQueryHandler, CreateCategoryCommandHandler createCategoryCommandHandler, UpdateCategoryCommandHandler updateCategoryCommandHandler, RemoveCategoryCommandHandler removeCategoryCommandHandler)
//        {
//            _getCategoryQueryHandler = getCategoryQueryHandler;
//            _getCategoryByIdQueryHandler = getCategoryByIdQueryHandler;
//            _createCategoryCommandHandler = createCategoryCommandHandler;
//            _updateCategoryCommandHandler = updateCategoryCommandHandler;
//            _removeCategoryCommandHandler = removeCategoryCommandHandler;
//        }

//        [HttpGet]
//        public async Task<IActionResult> CategoryList()
//        {
//            List<GetCategoryQueryResult> values = await _getCategoryQueryHandler.Handle();
//            return Ok(values);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetCategory(int id)
//        {
//            GetCategoryByIdQueryResult value = await _getCategoryByIdQueryHandler.Handle(new GetCategoryByIdQuery(id));
//            return Ok(value);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
//        {
//            await _createCategoryCommandHandler.Handle(command);
//            return Ok("Ekleme işlemi basarılı");
//        }

//        [HttpPut]
//        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
//        {
//            await _updateCategoryCommandHandler.Handle(command);
//            return Ok("Güncelleme işlemi basarılı");
//        }

//        [HttpDelete]
//        public async Task<IActionResult> RemoveCategory( int id)
//        {
//            await _removeCategoryCommandHandler.Handle(new RemoveCategoryCommand(id));
//            return Ok("Silme işlemi basarılı");
//        }
//    }
//}
