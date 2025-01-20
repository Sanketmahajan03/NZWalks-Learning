namespace NZWalks_Learning.Model.DTO
{
    public class UpdateWalkDifficultyDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public bool IsDeleted { get; set; }
    }
}
