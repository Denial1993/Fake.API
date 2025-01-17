using System.Text.RegularExpressions;

namespace Fake.API.ResourceParameters
    {
    public class TouristRouteResourceParameters
        {
        public string? Keyword { get; set; }
        public string? RatinOperator { get; set; }
        public int? RatingValue { get; set; } //int? 表示 RatingValue 是一個可空變數
        private string _rating;
        public string? Rating
            {
            get { return _rating; }
            set
                {
                if (!string.IsNullOrWhiteSpace(value))
                    {
                    Regex regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)"); //正則表達式
                    Match match = regex.Match(value);
                    if (match.Success)
                        {
                        RatinOperator = match.Groups[1].Value;
                        RatingValue = Int32.Parse(match.Groups[2].Value);
                        }
                    }
                _rating = value;
                } //value 是 Rating內建的，負責接收外界的數據
            }
        }
    }
