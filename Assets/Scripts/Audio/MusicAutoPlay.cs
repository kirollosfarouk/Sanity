using System.Threading.Tasks;
using UnityEngine;

public class MusicAutoPlay : MonoBehaviour
{
    public MusicDefinition Music;
    private async void Start()
    {
        await Task.Delay(500);
        MusicPlayer.Instance.StartMusic(Music);
    }
}