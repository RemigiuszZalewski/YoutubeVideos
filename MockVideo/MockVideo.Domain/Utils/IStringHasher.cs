namespace MockVideo.Domain.Utils;

public interface IStringHasher
{
    string Hash(string password);
    bool HashesMatch(string hash, string providedPassword);
}