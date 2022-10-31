using ConvertVoiceToTextBot.Extensions;
using FFMpegCore;
using System.IO;

namespace ConvertVoiceToTextBot.Utilities
{
    public static class AudioConverter
    {
        public static void TryConvert(string inputFile, string outputFile)
        {
            // Задаём путь, где лежит вспомогательная программа - конвертер
            GlobalFFOptions.Configure(options => options.BinaryFolder = Path.Combine(DirectoryExtension.GetSolutionRoot(), "ffmpeg-win64", "bin"));

            // Вызываем Ffmpeg, передав требуемые аргументы.
            FFMpegArguments.FromFileInput(inputFile).OutputToFile(outputFile, true, options => options.WithFastStart()).ProcessSynchronously();
        }
    }
}
