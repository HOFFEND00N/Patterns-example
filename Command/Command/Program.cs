using System;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartAssistant Siri = new SmartAssistant();
            MusicPlayer player = new MusicPlayer();
            StartMusicCommand start = new StartMusicCommand(player);
            EndMusicCommand end = new EndMusicCommand(player);

            Siri.SetCommand(start);
            Siri.ExecuteCommand();

            Siri.SetCommand(end);
            Siri.ExecuteCommand();
        }

        interface ICommand
        {
            void Execute();
        }

        class SmartAssistant
        {
            ICommand command;
            public void SetCommand(ICommand command)
            {
                this.command = command;
            }

            public void ExecuteCommand()
            {
                command.Execute();
            }
        }

        class StartMusicCommand : ICommand
        {
            MusicPlayer musicPlayer;

            public StartMusicCommand(MusicPlayer musicPlayer)
            {
                this.musicPlayer = musicPlayer;
            }

            public void Execute()
            {
                musicPlayer.SetMusicVolume(5);
                musicPlayer.On();
            }
        }

        class EndMusicCommand : ICommand
        {
            MusicPlayer musicPlayer;

            public EndMusicCommand(MusicPlayer musicPlayer)
            {
                this.musicPlayer = musicPlayer;
            }

            public void Execute()
            {
                musicPlayer.Off();
            }
        }

        class MusicPlayer
        {
            private int volume;
            public void SetMusicVolume(int volume)
            {
                this.volume = volume;
                Console.WriteLine("Volume set to " + volume);
            }

            public void On()
            {
                Console.WriteLine("Music player starts working");
            }

            public void Off()
            {
                Console.WriteLine("Music player stops working");
            }
        }
    }
}
