namespace fragrance_API.dtos.Fragrance
{
    public class UpdateRatingRequest
    {
        public string username { get; set; }
        public int fragranceId { get; set; }
        public double newRating { get; set; }
    }
}
