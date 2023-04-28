using DahlexApp.Logic.Interfaces;
using Plugin.Maui.Audio;

namespace DahlexApp.Logic.Services;

public class SoundService : ISoundService
{
    public SoundService(IAudioManager audio)
    {
        Audio = audio;
    }

    public async Task Init()
    {
        Stream bombStream = await FileSystem.OpenAppPackageFileAsync("bomb.wav");
        BombPlayer = Audio.CreatePlayer(bombStream);

        var teleStream = await FileSystem.OpenAppPackageFileAsync("tele.wav");
        TelePlayer = Audio.CreatePlayer(teleStream);

        var crashStream = await FileSystem.OpenAppPackageFileAsync("heap.wav");
        CrashPlayer = Audio.CreatePlayer(crashStream);
    }

    private IAudioManager Audio { get; }
    private IAudioPlayer BombPlayer { get; set; } = null!;
    private IAudioPlayer TelePlayer { get; set; } = null!;
    private IAudioPlayer CrashPlayer { get; set; } = null!;

    public void PlayBomb()
    {
        if (!BombPlayer.IsPlaying)
        {
            BombPlayer.Play();
        }
    }

    public void PlayTele()
    {
        if (!TelePlayer.IsPlaying)
        {
            TelePlayer.Play();
        }
    }

    public void PlayCrash()
    {
        if (!CrashPlayer.IsPlaying)
        {
            CrashPlayer.Play();
        }
    }
}