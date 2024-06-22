using EmploeeAssignment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmploeeAssignment.ServiceFilter
{
    public class BuildEmployeeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is StudentFilterCriteria);
            if(param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }

            StudentFilterCriteria studentFilterCriteria = (StudentFilterCriteria)param.Value;
            var statusFilter = studentFilterCriteria.Filters.Find(a => a.FieldName == "status");
            if(statusFilter==null)
            {
                statusFilter = new FilterCriteria();
                statusFilter.FieldName = "status";
                statusFilter.FieldValue = "Active";
                studentFilterCriteria.Filters.Add(statusFilter);
            }
            studentFilterCriteria.Filters.RemoveAll(a => string.IsNullOrEmpty(a.FieldName));
            var result = await next();
        }
       
    }
}
