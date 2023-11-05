using Microsoft.AspNetCore.Mvc;
using SmartContractLib.Data;
using SmartContractLib.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly ContractService contractService;

        public ContractController(ContractService contractService)
        {
            this.contractService = contractService;
        }

   

        // POST api/<MessageController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ContractModel contract)
        {
            contractService.CreateContract(contract);
            return Ok();
        }

        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        public ActionResult<ContractModel> Get(string id)
        {

          var contract=  contractService.ReadContract(id);
            if(contract.IsEmpty==false)
            {
                return NotFound(contract);
            }

            return Ok( contract);
       
        }

        // PUT api/<MessageController>/5
        [HttpPut("{contractname}")]
        public ActionResult Put(string contractname, [FromBody] MessageModel message)
        {
            var contract = contractService.ReadContract(contractname);
            
            return Ok();
        }
    }
}
