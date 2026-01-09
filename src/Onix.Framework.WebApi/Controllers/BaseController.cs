using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Onix.Framework.Domain.Interfaces;
using Onix.Framework.Notifications.Interfaces;

namespace Onix.Framework.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController(INotificationContext notificationContext, IExceptionProcessor exceptionProcessor) : ControllerBase
    {
        protected readonly INotificationContext _notificationContext = notificationContext;
        protected readonly IExceptionProcessor _exceptionProcessor = exceptionProcessor;

        protected async Task<IActionResult> TryExecuteAsync<T>(Task<T> action)
        {
            try
            {
                var result = await action;
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                _notificationContext.AddError(ex.Message);
                return BadRequest(new { notifications = _notificationContext.GetNotifications() });
            }
        }

        protected async Task<IActionResult> TryExecuteNoResultAsync(Task action)
        {
            try
            {
                await action;
                return Ok(new { notifications = _notificationContext.GetNotifications() });
            }
            catch (Exception ex)
            {
                _notificationContext.AddError(ex.Message);
                return BadRequest(new { notifications = _notificationContext.GetNotifications() });
            }
        }

        protected IActionResult HandleResponse(object result)
        {
            return Ok(new { result, notifications = _notificationContext.GetNotifications() });
        }
    }
}
