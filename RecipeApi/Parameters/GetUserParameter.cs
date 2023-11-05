namespace RecipeApi.Parameters
{
    public class GetUserParameter : GetParameter
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public override void TransformTo()
        {
            throw new NotImplementedException();
        }
    }
}
