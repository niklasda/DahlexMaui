﻿using Plugin.Maui.Audio;

namespace DahlexApp.Views.Board
{
    public interface ISoundManager
    {
        void PlayBomb();
        void PlayTele();
        void PlayCrash();
    }

    public class SoundManager : ISoundManager
    {
        public SoundManager(IAudioManager audio)
        {
            _audio = audio;
        }

        public async Task Init()
        {
            Stream bombStream = await FileSystem.OpenAppPackageFileAsync("bomb.wav");
            _bombPlayer = _audio.CreatePlayer(bombStream);

            var teleStream = await FileSystem.OpenAppPackageFileAsync("tele.wav");
            _telePlayer = _audio.CreatePlayer(teleStream);

            var crashStream = await FileSystem.OpenAppPackageFileAsync("heap.wav");
            _crashPlayer = _audio.CreatePlayer(crashStream);
        }

        private IAudioManager _audio { get; }
        private IAudioPlayer _bombPlayer { get; set; }
        private IAudioPlayer _telePlayer { get; set; }
        private IAudioPlayer _crashPlayer { get; set; }

        public void PlayBomb()
        {
            if (!_bombPlayer.IsPlaying)
            {
                _bombPlayer.Play();
            }
        }

        public void PlayTele()
        {
            if (!_telePlayer.IsPlaying)
            {
                _telePlayer.Play();
            }
        }

        public void PlayCrash()
        {
            if (!_crashPlayer.IsPlaying)
            {
                _crashPlayer.Play();
            }
        }
    }
}
