using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Extensions
{
    public static class HttpContextExtension
    {
        public async static Task InsertParametersPaginationHeaders<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            // select count(*) from publihser
            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalAmountOfRecords", count.ToString());
        }
    }
}
