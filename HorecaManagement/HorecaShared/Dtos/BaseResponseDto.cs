namespace Horeca.Shared.Dtos
{
    public class BaseResponseDto
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
    }
}