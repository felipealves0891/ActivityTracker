﻿namespace ActivityTracker.Api.Models.Dtos
{
    public class UpdateActivityDto
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public DateTime Created { get; set; }
    }
}
