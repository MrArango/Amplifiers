namespace AmplifiersAPI.Models
{
    public class Respon
    {
        public bool Valid { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }


        public Respon()
        {
            Valid = true;
        }
    }
}
