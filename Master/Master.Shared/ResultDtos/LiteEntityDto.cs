namespace Master.Shared.ResultDtos
{
    public class LiteEntityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ServiceDuration { get; set; }
    }

    public class LiteEntityWithCodeDto : LiteEntityDto
    {
        public string Code { get; set; }
    }
}
