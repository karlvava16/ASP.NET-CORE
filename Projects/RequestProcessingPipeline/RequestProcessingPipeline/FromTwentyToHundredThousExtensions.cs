namespace RequestProcessingPipeline
{
    public static class FromTwentyToHundredThousExtensions
    {
        public static IApplicationBuilder UseFromTwentyToHundredThous(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromTwentyToHundredThousMiddleware>();
        }
    }
}
