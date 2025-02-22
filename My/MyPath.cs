using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace My
{
    public class MyPath
    {
        public static string GetFileName(string directory)
        {
            if (string.IsNullOrEmpty(directory)) return string.Empty;

            char separator = directory.Contains('\\') ? '\\' : '/'; // 경로구분자가 Windows는 '\\', Mac은 '/'. + '\\' 는 string이 아니라 char 구나.

            int lastIndex = directory.LastIndexOf(separator);

            if (lastIndex == -1) return directory; 
            // lastIndeoxOf 에서 조회가 안될 경우, -1을 반환.
            // + (return directory.SubString(lastIndex + 1); 로 해도 결과는 같을텐데, if(bool) 연산이 Substring보다 적은 건가.. lastIndex == -1  이 나올 확률이 매우 적을텐데, 굳이 
            // 이렇게 처리할 필요 있나 싶음

            return directory.Substring(lastIndex + 1);
        }

        public static string GetFileNameWithoutExtension(string directory)
        {
            if (string.IsNullOrEmpty(directory)) return string.Empty;

            char separator = directory.Contains('\\') ? '\\' : '/';

            int lastIndex = directory.LastIndexOf(separator);

            int dotIndex = directory.LastIndexOf('.');

            if (dotIndex == -1 || lastIndex > dotIndex) return directory.Substring(lastIndex + 1);

            return directory.Substring(lastIndex + 1, dotIndex - lastIndex - 1);
        }

        public static string GetExtension(string directory)
        {
            if (string.IsNullOrEmpty(directory)) return string.Empty;

            int dotIndex = directory.LastIndexOf('.');

            if (dotIndex == -1 || dotIndex == 0) return string.Empty;   // 숨김파일의 경우, . 의 index는 0이며, . 뒤에 있는 문자열은 확장자가 아닌 파일명이기 때문에, string.Empty를 반환

            return directory.Substring(dotIndex + 1);
        }


        public static string GetDirectoryName(string directory)
        {
            if (string.IsNullOrEmpty(directory)) return string.Empty;

            if (directory.Length == 1 || (directory.Length == 3 && directory[1] == ':' && directory[2] == '\\')) return directory;
            // Mac 의 루트 디렉터리는 '/' 이고, Window의 루트 디렉터리는 "C:\\" , "D:\\" 등이다. 따라서 루트디렉터리를 인자로 했을 경우 directory 반환


            char separator = directory.Contains('\\') ? '\\' : '/';

            int lastIndex = directory.TrimEnd(separator).LastIndexOf(separator);
            // 파일이 아닌 폴더명을 인수로 했을 경우, 마지막 폴더명이 아닌 부모 폴더명을 반환하는 매서드라 하기 때문에, TrimEnd로 마지막이 \\ 인 경우에 제거하고 lastIndexOf 처리

            if (lastIndex == -1) return string.Empty; // 이런 경우가 있을까 싶어서, 넣을지 말지 고민했는데, 아마 인수로 디렉터리가 아닌, 파일명만 실수로 입력햇을 때 반환하는 용도?

            return directory.Substring(0, lastIndex);
        }
    }
}
