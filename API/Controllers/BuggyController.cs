using API.Errors;
using Infrastracture.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly ILogger<BuggyController> _logger;

        public BuggyController(StoreContext context, ILogger<BuggyController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("notFound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing=_context.Products.Find(42);
            if (thing==null){
                return NotFound(new ApiResponse(404));
            }else{
              return Ok();  
            }
        }



        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing=_context.Products.Find(42);
            var ret= thing.ToString();
            return Ok();  
        }


        [HttpGet("badrequest")]
        public ActionResult getBadRequest()
        {
              return BadRequest(new ApiResponse(400));  
        }      

        [HttpGet("badrequest/{id}")]
        public ActionResult getBadRequest2(int id)
        {
              return Ok();  
        }            

    }
}