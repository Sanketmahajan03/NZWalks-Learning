﻿namespace NZWalks_Learning.Model.DTO
{
    public class AddWalksRequestDto
    {
        public string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}