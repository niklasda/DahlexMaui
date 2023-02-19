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
        // TODO  use this to prevent parallell play
        public SoundManager(IAudioManager audio)
        {
            _audio = audio;

        }

        public async Task Init()
        {


            Stream bombStream = await FileSystem.OpenAppPackageFileAsync("bomb.wav");
            bombPlayer = _audio.CreatePlayer(bombStream);

            var teleStream = await FileSystem.OpenAppPackageFileAsync("tele.wav");
            telePlayer = _audio.CreatePlayer(teleStream);

            var crashStream = await FileSystem.OpenAppPackageFileAsync("heap.wav");
            crashPlayer = _audio.CreatePlayer(crashStream);

        }

        private readonly IAudioManager _audio;
        private IAudioPlayer bombPlayer;
        private IAudioPlayer telePlayer;
        private IAudioPlayer crashPlayer;

        public void PlayBomb()
        {
            // ImageSource.FromFile();

            //  Stream stream = await FileSystem.OpenAppPackageFileAsync("bomb.wav");
            //IAudioPlayer audioPlayer = _audio.CreatePlayer(stream);

            if (!bombPlayer.IsPlaying)
            {
                bombPlayer.Play();

            }

            //ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
            //  _audio.Load(GetStreamFromFile("bomb.wav"));
            //player.Play();
        }

        public void PlayTele()
        {
            if (!telePlayer.IsPlaying)
            {
                telePlayer.Play();

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
            if (!crashPlayer.IsPlaying)
            {
                crashPlayer.Play();

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
