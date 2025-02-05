﻿namespace DVDRetal.Model.ResponseModels
{
    public class ManagerResponseModel
    {


        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int CopiesAvailable { get; set; }
        public string ImageUrl { get; set; }
    }
}
