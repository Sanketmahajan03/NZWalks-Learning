﻿namespace NZWalks_Learning.Model.Domain
{
    public class Walks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        public bool IsDeleted { get; set; }

        //Navigation properties
        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }

    }
}
