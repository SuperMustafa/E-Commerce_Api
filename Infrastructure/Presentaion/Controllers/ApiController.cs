using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Product;
using Shared.ErrorModels;

namespace Presentaion.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProductResultDto), (int)HttpStatusCode.OK)]
    public class ApiController:ControllerBase
    {
    }
}
