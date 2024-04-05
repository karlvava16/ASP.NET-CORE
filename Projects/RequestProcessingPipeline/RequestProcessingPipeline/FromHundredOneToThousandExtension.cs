namespace RequestProcessingPipeline
{
    public static class FromHundredOneToThousandExtension
    {
        public static IApplicationBuilder UseFromHundredOneToThousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromHundredOneToThousandMiddleware>();
        }
    }
}
