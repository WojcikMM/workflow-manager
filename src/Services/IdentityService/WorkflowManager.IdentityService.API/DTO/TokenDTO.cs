namespace WorkflowManager.IdentityService.API.DTO
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public TokenDTO(string Token)
        {
            this.Token = Token;
        }
    }
}
