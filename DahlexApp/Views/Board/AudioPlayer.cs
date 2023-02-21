using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // ImageSource.FromFile();

            //  Stream stream = await FileSystem.OpenAppPackageFileAsync("bomb.wav");
            //IAudioPlayer audioPlayer = _audio.CreatePlayer(stream);

            if (!_bombPlayer.IsPlaying)
            {
                _bombPlayer.Play();

            }

            //ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
            //  _audio.Load(GetStreamFromFile("bomb.wav"));
            //player.Play();
        }

        public void PlayTele()
        {
            if (!_telePlayer.IsPlaying)
            {
                _telePlayer.Play();

            }

//            var stream = await FileSystem.OpenAppPackageFileAsync("tele.wav");
  //          using (IAudioPlayer audioPlayer = _audio.CreatePlayer(stream))
    //        {
      //          audioPlayer.Play();
        //    }
            //ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
            //  player.Load(GetStreamFromFile("tele.wav"));
            //player.Play();
        }

        public void PlayCrash()
        {
            if (!_crashPlayer.IsPlaying)
            {
                _crashPlayer.Play();

            }

//            var stream = await FileSystem.OpenAppPackageFileAsync("heap.wav");
  //          var audioPlayer = _audio.CreatePlayer(stream);
    //        audioPlayer.Play();

            //        IAudioManager player = CrossSimpleAudioPlayer.Current;
            // var v = player.Volume;
            //          player.Load(GetStreamFromFile("heap.wav"));
            //        player.Play();
        }
    }
}
