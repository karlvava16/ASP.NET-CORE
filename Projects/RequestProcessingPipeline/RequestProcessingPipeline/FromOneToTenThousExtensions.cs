namespace RequestProcessingPipeline
{
    public static class FromOneToTenThousExtensions
    {
        public static IApplicationBuilder UseFromOneToTenThous(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromOneToTenThousMiddleware>();
        }
    }
}
