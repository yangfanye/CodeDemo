using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WeiXinZhuaFaWang
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            string key1 = @"31 36 34 39 0d 0a 1f 
    8b 08 00 00 00 00 00 00  03 b5 5b 7b 73 da ba b6 
    ff ff ce dc ef d0 cd dc  32 76 ac b8 d8 18 42 70 
    94 4c 9a 34 4d da e6 d1  24 7d ec cd 90 8e b1 0d 
    18 8c 6d 0c 24 4d 02 e7  b3 9f b5 24 f9 09 ed 6e 
    f7 39 77 77 b6 91 65 e9  a7 a5 a5 f5 94 94 7b 2b 
    7e 71 14 3f 46 f3 f0 dd  0d 4d 0a cb 65 7f 11 d8 
    73 2f 0c a4 90 b8 f2 f3  3d 34 1a d2 e7 15 19 d0 
    a1 ea 7b 3d 2c 3e d1 b4  8d 0c af 8f 74 a0 be b6 
    66 2e 7d 76 bf cf dd c0  69 a7 5f 7b f2 f3 93 1a 
    c5 e1 3c 9c 3f 46 2e 9d  0f bd 99 89 80 0e 0d dc 
    87 17 4f 66 af 5a 75 d4  89 f7 fd 0c 9b 9a 8e 3a 
    b4 66 97 0f c1 55 1c 46  6e 3c 7f 94 2a 5e e0 cd 
    2b f2 72 29 39 2a 16 f3  c3 3a ea ff cd 16 d0 8c 
    7d 50 ad 28 f2 1f 25 c4  27 56 3c 58 4c dc 60 3e 
    93 57 08 c9 3e 67 24 38  66 d2 91 53 13 bb f3 45 
    1c bc 70 56 c4 8e 5d 6b  ee b6 73 43 20 a5 3d d6 
    4c e5 13 93 64 b3 97 1f  af 97 1b 2c 41 ea ad 08 
    b6 68 17 38 c4 a6 58 60  4b 3f 8c 25 c6 89 17 1e 
    f4 91 9f 7b e5 b9 3b 72  b5 ca 26 d4 71 ba b4 07 
    0f 79 b5 d6 a6 32 0f 6f  e6 b1 17 0c 2a 49 63 35 
    a9 a1 bd b4 28 c3 dc fc  30 28 4c 4d 10 cb ba 14 
    39 94 4c 15 3f c9 ab 15  b1 60 71 bf 84 b1 73 18 
    c7 d6 23 7d 4c 3e 3f 17  27 d9 23 0e cc 81 f3 ea 
    01 5a cf 68 6f b9 ec 74  4d 56 31 f3 06 af 1f e7 
    ee 8c 3a 7f 50 f7 c0 69  1b 5b 3d d5 77 83 c1 7c 
    b8 22 09 91 05 e6 70 ea  24 80 98 c8 ea 8c 7d f7 
    fa 8f 82 24 62 87 81 6d  cd 0b 1d 90 93 51 6e 74 
    12 c0 fc 79 c9 a7 05 1a  cc 1e 7c 49 5f d8 27 db 
    b7 26 11 2c ad d7 97 fc  97 46 6e 69 68 cd 74 f6 
    7a a6 a3 28 f2 73 d4 f1  15 67 7f 7f 5f ef 2e a9 
    14 74 78 11 1f c6 76 6b  4b 72 a0 5f 55 6f 34 e4 
    bd 3d 5e 21 41 6b 19 2a  57 2b d7 9f b9 cf 00 dd 
    6c 34 ea 8d bd 40 4c 9c  8f 92 8d 40 8d fc 10 34 
    19 40 74 8f d4 68 31 1b  0a a1 8b 48 00 b0 85 49 
    29 b4 67 e6 56 14 d7 1b  a6 f4 03 51 e6 6c 71 ca 
    6c 11 23 56 a9 a1 ef 1a  bb cd 1d 7d b7 b1 b7 57 
    d7 93 d9 99 c9 92 d1 50  b5 5d cf 97 9c 57 c6 26 
    c1 e2 03 3d aa ec 83 6a  5b be cf 97 cd 14 eb 91 
    a3 41 9d f9 9e ed 4a b5  bc ea c4 56 e0 84 93 8d 
    9a e2 d3 4e 97 14 39 e6  33 be 48 29 c5 cd ad 50 
    e5 08 92 bc ac c9 2b 81  8b b6 c6 62 62 2e f9 a4 
    07 cc 93 c9 18 8c 99 1b  d8 68 cc 26 74 ac 9e ba 
    df e9 73 2a 68 1b 44 4b  50 5f 94 9e 84 b2 00 29 
    f3 81 32 1f 28 f3 51 5a  b8 f8 c0 82 16 c5 c4 17 
    62 62 06 9c 70 09 79 6e  c8 a9 a6 4a 5a 53 96 b3 
    8f 55 ad 51 fa 96 ce 48  1d 85 5e 20 55 2a b0 04 
    91 15 cf dc 8d 1c 43 25  e0 8b 46 72 cc 0b 90 79 
    3a 30 8f ad 78 1d e4 99  21 9c 05 73 09 e6 b6 e8 
    01 1b 24 87 e8 32 81 01  99 38 1b 28 01 ad cd dc 
    0c 5e e9 68 24 3c e0 e1  07 6b ee 05 da cf d8 18 
    fc 9c 8d a5 05 56 d2 f5  e5 0c 50 fb 71 38 39 1a 
    5a f1 51 e8 b8 3f 51 c1  94 50 ff 3f 64 92 92 f0 
    88 e9 7c 4f b5 c5 d8 87  73 34 ce 79 6d c7 b1 37 
    b3 07 99 33 02 e6 7c 9a  f7 5b 3f 62 cd 3c 7e 4c 
    ec b1 e3 da 30 c0 a7 eb  b3 a3 70 12 81 02 c1 9a 
    b8 33 db 8a 5c c9 cb d9  c1 9e 0c 93 04 13 68 0f 
    81 90 e7 f9 30 0e 1f 5e  bc 89 63 98 50 e5 dc f2 
    61 62 13 d7 79 f1 e9 f6  64 bb f5 c2 b1 e6 56 05 
    89 58 67 80 18 d2 53 d9  27 69 11 88 91 40 2d ca 
    34 b0 01 41 d9 d1 cf 2f  fa 7d 37 76 9d d7 7e 68 
    8f 0f fd 41 18 7b f3 e1  24 e7 17 62 77 e6 16 bc 
    1f 53 f8 6f 48 08 cd 78  c3 2d ef b7 e0 18 aa b9 
    6f a8 ad c8 37 30 70 e5  f0 a1 c2 a7 5d a1 14 9d 
    53 d8 7f 01 21 83 d4 a3  23 41 35 50 66 66 03 a8 
    dc 35 60 2c 51 c6 57 72  12 07 23 81 bb b3 dd d9 
    2c 1b ca e7 22 3a a7 19  1a 01 63 25 ac 25 f8 96 
    b4 37 f8 16 d6 a6 87 1c  b8 f1 9e 5c 10 99 e8 95 
    64 6c 05 32 94 fc 83 c4  3e ca ed 50 9d 58 df 41 
    8f c1 14 6d 73 d8 89 17  70 06 b2 7e 60 f9 7c ea 
    6c 05 66 04 46 15 3e 01  86 4f 22 ee 88 32 01 8d 
    41 20 e3 3d                                      
    df 8c 15 1a a4 dc 0c af  f8 04 d8 32 48 33 12 83 
    fc 51 b0 a8 11 37 a9 c4  07 0e a4 14 6f d3 68 83 
    74 c6 30 d4 ef 19 70 be  88 79 76 63 1b 29 6f bf 
    8b 33 6c d7 20 04 1b a8  a7 d6 6c 08 f1 96 9d ca 
    88 dd 1f b4 53 89 91 4b  f1 52 4f 4c 12 1a d1 a4 
    90 b4 4d 16 96 09 99 04  e4 af 49 9b cd bf e5 09 
    4f 58 76 9d 74 5a 44 4e  21 d2 4b 46 14 f2 97 89 
    8f 90 92 6c 86 dc bb f6  bd c0 f2 71 7a 79 04 90 
    cb 32 48 3e c8 82 f1 4f  44 37 24 21 15 9e b6 d6 
    24 df 78 e8 79 ea fa 10  d6 6d d2 d1 b4 ca 81 85 
    4d a2 23 5c ca 9e 30 34  b2 ac 26 44 81 e0 81 aa 
    26 90 13 cb fe 07 b0 7d  f5 f4 fc f0 88 63 f7 c8 
    1a 3a 2c 2a 4a 49 1f 7c  a8 05 16 00 9c 68 32 d5 
    e1 4a 3a b7 20 ba 31 a5  92 44 59 69 7e 41 7a d4 
    c2 3c 22 0b 2a 4d 0b 5d  31 cb 20 9a c6 46 23 39 
    e2 20 2e a8 3d d7 47 0f  4a a9 3e 0a 31 f9 36 b1 
    22 73 94 46 73 23 30 e6  a9 6b 19 82 0e 0d f7 3c 
    73 a8 d0 7a a6 59 36 95  dc ce b0 e8 46 86 59 24 
    a7 35 97 f8 5d d1 8a 2d  24 a8 91 b3 56 ad 25 b6 
    d1 d7 da e8 49 1b d2 87  c1 8d fd 7e b5 3a 54 6a 
    ea 4e 63 ab 0f 84 f4 d1  bb 8c b8 7b 1b 30 cf 02 
    5e c5 86 fe cd 2d a9 be  dd 97 ab cd 3a 9a 5c b0 
    04 2e 4d bf 37 0d 99 13  0f d3 e4 2e eb a5 61 a6 
    30 2e b4 17 cb 30 fa b1  e7 1b 73 56 f6 c1 27 09 
    af e7 65 ec 23 43 ea e5  06 33 87 60 6b 87 d0 d2 
    0b 1c f7 fb 65 5f 1a ca  64 5b fb 83 62 35 2c 3e 
    98 de 8c bf e0 39 47 b4  46 5c 98 ab bb d7 37 5d 
    9c 1f 52 8f 01 35 b6 18  00 72 02 33 4e c6 70 b7 
    35 19 9d e8 16 6b 07 fe  65 53 1b 59 46 b6 6c 8b 
    46 e6 b0 33 4a 5c f2 60  69 a7 2e 78 84 df 46 8a 
    92 f2 00 fc 35 53 01 69  48 46 32 1a 26 2b 6a 57 
    0e 5f 1f 1d bf 39 79 7b  7a f6 ee fd 87 f3 8b cb 
    ab 8f d7 37 b7 9f 3e 7f  f9 fa e7 5f 56 cf 76 dc 
    fe 60 e8 8d c6 fe 24 08  a3 69 3c 9b 2f ee 1f be 
    3f 3e d5 34 bd 6e 34 9a  3b ad 5d e5 15 ad 80 f0 
    4b 79 f1 06 7e 26 e5 17  36 28 12 24 1c 24 26 53 
    12 91 10 72 54 ea 28 d2  ac ea 2f ff 35 ab c6 b2 
    32 55 42 a1 28 92 b3 b7  17 2d 59 fc a5 6f 47 b2 
    32 5b a5 20 fd 1f 80 c4  4b bf fa af 5f 05 71 37 
    83 dc f9 77 bf 8a 10 6c  44 f0 ef a4 d9 12 a8 f8 
    1b 10 21 15 93 4c e9 2d  3a 41 a5 07 61 b3 32 c5 
    07 89 b1 84 73 60 0d d0  98 80 36 83 28 a1 be 36 
    8d fd 21 28 2c c8 d1 a0  33 ec d2 5c 90 3f 56 ad 
    de 0c 24 64 06 32 8e ca  08 b1 fe ca 02 a8 f3 e3 
    06 d8 85 c4 c9 24 16 7f  3d 16 81 2c 7a c8 62 11 
    8f 9b b8 8e b6 53 d7 1b  2d 43 db ad 13 a3 a6 d7 
    f5 7a dd d0 76 88 de 68  ea f5 56 5d ab e9 44 df 
    81 26 f5 d6 4e ab 8b a2  54 f4 be 19 fe 3b f2 21 
    33 2e 9f 60 0a 5a 73 ff  93 f9 29 49 08 6e e9 07 
    e5 13 b9 a1 ef 3a b7 5d  13 1f 54 ba 41 fb 71 c3 
    cc 86 5c d5 9a 3b 1a 90  d0 58 62 b5 6e b0 fa 96 
    5c 35 f4 9d 16 98 91 7a  b3 b6 e2 b8 d9 24 84 35 
    bc 05 c8 0f 4a ad cb b0  3f 28 5a 97 5c b2 82 de 
    25 17 ac 50 ef 82 d5 c5  82 d1 25 a7 ac d0 e8 92 
    47 56 68 62 e6 82 85 9d  2e 09 59 a1 d5 25 ef 59 
    61 b7 4b ce 38 20 40 bf  e5 25 c0 8e 78 09 c0 4f 
    78 09 d0 67 bc 64 60 f8  cc 4a 80 7f 4d 3f 75 a0 
    e3 39 fc 40 af 8f f0 03  5d ae e0 a7 8e df 6c e9 
    9a 9c 93 8f e4 8a dc 92  1d 32 80 96 32 7c b4 a5 
    2b c2 eb 6f 88 a6 43 b5  06 d5 1f a1 1a 1b e2 87 
    4b a2 61 6b 1d aa cf a1  9a 23 5c 93 0b a2 63 eb 
    3a 54 e7 a1 7b 0c da 28  41 9f 72 e8 46 09 fa 91 
    43 37 4b d0 3e 87 de 29  41 87 0c ba 55 82 7e cf 
    a1 77 4b d0 67 1c 5a ab  95 b0 df 72 6c 4d 2b 81 
    47 0c 5c d3 4b e8 27 82  27 f5 12 fc 4c c0 1b 25 
    78 47 c0 37 18 7c 3f 85  bf 21 0d ac 6e 32 f8 7e 
    0a ff 48 76 b1 7a 87 a1  f7 53 f4 b7 44 33 b0 be 
    c5 d0 fb 29 fa 2d d1 6b  58 bf 5b 42 3f 65 e8 7a 
    ad 84 7e c6 d0 75 ad 84  ee 70 74 5d 2f a1 f7 38 
    ba 5e 2f a1 bf e7 e8 46  09 7d c6 d1 1b 25 f4 0b 
    81 de 2c a1 87 02 7d a7  84 7e c2 d1 5b 25 f4 4b 
    8e be 5b 42 f7 39 7a bd  56 42 8f 38 7a 9d 2f ab 
    9b e3 0c 6b ce 97 d5 4d  d1 43 a2 69 58 cf 97 d5 
    cd 33 be 89 f5 7c 59 dd  14 7e 46 f4 3a d6 37 4a 
    f0 37 1c be 59 82 ef 09  f8 9d 12 bc 2f e0 5b 25 
    f8 33 01 bf 5b 82 3f 61  f0 46 ad 04 7f cb e1 0d 
    ad 04 7f c1 e1 0d bd 04  ff c8 e1 8d 7a 09 fe 3d 
    87 37 4a f0 91 80 6f 94  e0 1d 01 df 2c c1 5f 0a 
    78 be b2 41 ce ca b0 e6  7c 65 83 14 1e 98 80 4b 
    65 f0 a5 0d f2 2a 85 92  d0 e0 4b 1b a4 f0 a7 44 
    47 72 1a 5a 09 3e 62 f0  0d bd 04 7f c1 e1 1b f5 
    12 fc 99 80 37 4a f0 37  02 be 51 82 0f 39 7c b3 
    04 ef 08 f8 9d 12 fc a3  80 6f 95 e0 4f 04 fc 6e 
    09 be c7 e0 9b b5 12 fc  5b 0e df d4 4a f0 97 1c 
    be a9 97 e0 df 73 f8 26  cc d6 44 db cf 1c 80 72 
    bd ac 99 e8 02 98 1f 50  ce d9 9b de 65 ee 40 f9 
    c8 de ea 5d e6 15 94 ab  65 8d 79 d7 93 b5 fc 2a 
    d9 51 cb 25 e7 53 ea a4  3b bc ad ad 72 ba 0f 5e 
    aa b5 e5 64 7b 4c d3 4e  04 de b4 01 51 a3 a6 b7 
    58 cc 18 bd ac eb 2c 8d  09 21 c0 ed fb 21 b8 6d 
    ff 55 16 64 c8 d0 45 8a  14 08 42 f6 f7 77 f7 f6 
    0c 19 7d 1a                                      
    95 42 74 d8 e1 9a c3 0e  99 c3 0e cb 0e 7b 1d c3 
    00 0c 1f 31 fc 35 0c 9f  61 f8 6b 18 d9 2c a8 b1 
    25 4d 45 d8 0e 61 cf 5a  92 ea e4 42 03 33 e5 0f 
    0b d2 7d 96 83 f8 7c 77  32 a2 d3 8e df 25 f8 a0 
    52 84 d4 44 6b d4 44 8c  9a 68 2d 04 c9 9d 97 6c 
    da 39 70 e8 68 7d e7 c0  11 21 57 2e 70 29 ed 1c 
    38 98 53 4e 44 00 57 48  87 25 0b 3f 60 1e 5b fc 
    98 26 b6 d0 00 42 f2 cd  59 67 2f 0b 40 5d dc e6 
    83 00 b4 4f 5d 96 69 42  c0 e9 e6 22 51 fc cc c2 
    4f 1b aa df dc 47 ef 8f  4f 68 bf b0 5f 91 bd 8d 
    dd 47 96 b9 1b 64 c8 62  d7 b6 8b 84 13 6f ee c6 
    16 0e 3e 6b 6b ab f2 a6  c6 e0 27 9b 1a ec 80 26 
    9c 44 8b fc 9e c4 98 0c  b2 58 d2 4b fb 90 39 e4 
    48 7c d8 24 bf 91 41 01  ac ec 25 a4 be d0 0b 4c 
    e6 04 ad 10 77 43 ac 9b  12 68 86 42 8a f6 86 98 
    3f 56 ab 73 95 ef 89 40  a6 cd 74 62 44 d3 9a 71 
    2e f9 1f e0 ae 92 d8 7b  31 b3 28 5f 33 27 90 d2 
    4e 58 4a 0b 1d d3 e6 23  99 a4 cd 57 7e b2 39 37 
    c2 72 4e a4 87 89 10 f8  28 04 bd 94 fb 09 27 3c 
    32 20 c3 74 cf c2 4e d3  3a 59 15 4c c3 16 32 cf 
    cb 92 e5 66 1b 0c 47 5e  04 6c ca 9d 67 be 4e 52 
    d1 54 2a 46 90 5c f3 b4  64 c4 a5 e2 18 0a 99 54 
    1c 61 f5 c6 0d 4f 82 69  79 b6 6b 41 9e e0 15 e5 
    47 50 4f 0e a1 27 1f 9f  1e 15 c4 c8 cb b6 bd f8 
    44 de 04 36 a3 26 b7 43  63 93 5e f1 84 4e 4c 99 
    eb cf 9b 8b a3 6f 5f 4f  2e af cf bf 9d 5f 1e bf 
    21 d8 38 39 bc 3c 76 7f  13 eb f8 cd 06 ac a2 e4 
    fa e0 65 ec 9f 48 af 9d  d8 a1 ef b8 f1 7c 1e 3a 
    2e f5 45 0d 08 1f 75 fe  6e c3 ee e8 17 36 ec d6 
    76 6b ad d2 8e 9d 55 da  6c 4b 6d e2 a6 cd 3a e8 
    6c 95 36 eb ca fd 8b 9b  75 99 c2 7b f7 a2 50 5a 
    85 b6 46 4a bc 6c eb 3f  da d6 b3 92 f5 78 76 f9 
    d2 e7 17 0b 8f 0c 92 9d  b8 b5 ad 6f ff 60 dc 0e 
    65 55 74 93 2c c2 db af  88 e3 fe 23 20 d1 2d 03 
    62 5b 7b 23 f5 66 0e 84  4f 84 f8 1e e6 13 ea 4d 
    9e 79 23 e3 ff a8 15 37  39 c5 96 e1 04 d4 64 82 
    22 c2 6e 0d a4 20 11 09  88 d8 80 8f 85 97 f0 ee 
    cd f8 20 29 d2 d7 ed a4  3e 8a dd 7b a6 8a a9 fd 
    99 82 6b 9b ee f9 e6 94  1f d3 06 ca b4 7b 47 e3 
    ce b4 bb 82 c5 a7 12 68  30 36 e7 b3 61 e2 e9 65 
    0a f9 cb 1a 98 b6 49 f4  e7 77 d5 2e 6d 53 04 28 
    ea 1a eb c5 e7 69 73 ee  db 66 ca 83 1e 2c 8e 9c 
    5d 44 e8 67 34 e5 3c 55  b4 71 63 22 e5 6e 12 21 
    70 74 08 92 9c ec 18 c3  7c cc 74 10 d7 03 4f 22 
    9c 44 d8 f8 39 43 c0 ce  15 8a eb 40 03 71 8e eb 
    13 5f 89 d0 0e 03 69 e9  6c ff 96 34 3c cd 7e e6 
    a7 7f 05 d2 62 70 63 d9  09 cb 94 46 62 94 80 04 
    4a 2c 9b 7e 22 bc 9c 30  44 29 d0 8f 12 15 af d3 
    3a 65 71 06 9d a8 47 af  8f 68 df 04 f1 18 a8 91 
    e5 80 38 ca ea d5 d8 9e  ed d0 67 78 cd a8 c3 33 
    92 d4 0b 2f c0 59 45 64  41 17 db 71 ea bf 5e 2e 
    c8 8c 2e 58 ac b4 60 3b  c6 0b 0c a7 16 40 70 a7 
    0b be ba 66 ce f7 16 e6  9c 1d 56 4f f9 36 ed 4c 
    5e 2d e8 71 22 05 53 b2  00 d3 93 b8 c6 05 1e 4e 
    04 05 0a d8 d5 90 ec 20  47 1c a0 76 72 75 7c 7b 
    1a 77 9a 57 2b b3 20 ed  39 dd 45 d7 73 98 37 db 
    cf a8 86 ed 09 81 c1 1c  76 fd 02 42 96 35 db 7c 
    b8 6e 9b 73 4b 85 e1 88  03 cb e4 dd 43 04 e2 33 
    bd c6 a3 ab b2 37 a0 9b  dc 56 72 20 ec ab 25 1d 
    e4 17 1e b2 0f a9 24 91  0d 27 68 54 5b 89 5a 1c 
    28 e0 74 fa ac 25 71 f0  6a 11 63 d6 cf b6 ec 32 
    df c6 30 d4 bc 90 b2 8f  bf 92 90 20 5b 05 1f 7f 
    83 01 0e f6 91 72 19 4d  f1 54 91 b3 da a6 eb 76 
    95 71 68 c3 07 e2 a8 4c  78 c0 25 27 61 ba 9d 37 
    c2 86 30 c2 c3 34 36 b9  b2 62 6b 32 cb 19 c4 cd 
    27 71 c9 05 ad bf bd ad  c3 5a e3 bc ad 39 c4 9a 
    eb 57 77 40 c6 26 a8 72  bc 09 d3 ba 4b f0 bf 37 
    37 1f 36 1e fb 38 9c cb  36 58 29 6e 15 e6 40 27 
    64 39 90 11 59 fe 3c dd  7e 3e 48 b5 a9 a3 d5 77 
    5b ad dd 7a b3 65 10 6d  a7 a6 d5 76 9a 2d dc 85 
    49 f4 cb 49 4b b6 dc b6  73 77 2b dc f5 83 12 b0 
    98 3e e4 02 fc 98 d9 97  c5 b5 35 11 5b e3 32 67 
    63 51 ea 40 a2 5b ad 66  43 62 0d 8c 2b 84 3c a5 
    cf 11 46 4c 27 86 8c c6  35 3d ae 35 d0 a2 65 5a 
    ae 35 93 05 1c 26 5d 9f  33 0e b4 7d 82 f3 6f 07 
    2b 3c e9 0b 61 35 6f dc  d8 43 01 b5 7a be 2b 54 
    df db 1c 75 3e 73 d6 b7  27 b0 14 6b e1 c7 14 8c 
    26 1e 25 3f c7 6b 51 5e  cc 19 00 99 63 59 61 d9 
    e1 b3 19 80 81 4e e3 7e  b0 c5 11 bc 43 77 f3 67 
    d3 08 30 b0 6a cf 20 a8  6a 47 68 44 ac 24 b8 6e 
    4f 09 b3 4e 11 b3 29 a9  89 8a 12 25 cb c9 f4 34 
    e7 24 52 c1 6b c7 42 c2  56 9b 62 23 74 6d 53 b4 
    eb d1 da 2c 23 3c 90 17  7a c5 d7 9d 44 02 2a 0d 
    10 83 b2 61 92 10 2c 9b  bc 9f 93 55 b4 1e 25 b1 
    62 06 87 43 ad 85 65 ce  81 2d e4 cd 61 c6 40 6e 
    63 5e 4c 06 a8 32 63 a7  5f d4 17 f7 bb 6b 17 32 
    c6 6c 5e 4b 48 df 41 e8  c4 4d a8 16 5e 29 a2 4f 
    e9 0a 24 d1 ac af 4c 57  59 0e 15 a0 bb 9f 66 a2 
    1a 14 ee 68 f9 32 31 b6  a6 78 35 29 97 b8 f9 eb 
    ab 8b 0b 1a e0 82 4e b9  88 46 4c 44 c7 20 a2 57 
    d6 6c 86 88 98 2f 39 42  46 c3 82 8c 86 05 f7 04 
    f3 6d 0f 36                                      
    09 a9 c5 0e a4 20 68 08  d6 96 8f c9 5d 80 ac 52 
    05 77 20 30 b0 d2 1c d8  52 79 f8 8e d3 80 78 0a 
    65 ce b4 90 08 3e 44 2e  7a 60 63 60 3f 0c 2b 2c 
    61 ff a2 54 04 ac 0d 52  f5 73 b2 a6 05 a9 02 3d 
    4b a5 ea 57 08 26 53 66  f0 f2 74 0b 4a c2 24 0c 
    fa 01 f1 20 3e 2b a9 b8  2f 92 44 33 47 59 16 fc 
    1e 72 54 4c 98 73 a1 03  f9 08 75 6c 43 e4 1c 23 
    99 37 f8 38 c6 c7 19 3e  26 f8 38 c4 c7 0d 3f ad 
    83 c7 3b 7c 7c c0 c7 2d  3e ae 21 f6 d1 1b cd fd 
    6b f3 1a e3 f2 db ce 75  17 b7 de f6 af 0f ae 21 
    4e 6a e3 e3 4e 6f d5 d3  b3 c2 2b 5a 23 97 f0 7f 
    a9 1f 7e ba a0 97 77 97  d8 1e 1e 3a 3e ea f8 30 
    c8 05 bd c0 ed a9 bb 0b  8c 7d ee 76 77 cd f3 ce 
    55 97 5e 98 6f 3a 17 5d  7a 25 ae 28 dc 42 1d 79 
    0d 3f 7d 3c df ba ed bc  ee 82 3e e9 8d 9d ad 5b 
    68 75 a7 81 f5 ae d7 6a  ad ad 0b f3 18 3b 0f 58 
    24 37 40 58 f3 4c 54 40  50 87 15 5a d3 9c 88 9a 
    16 ab d0 0d f3 90 55 98  03 2a 70 76 b7 c2 3b bc 
    e7 b9 b3 f5 fa 0e c7 e8  67 03 5c 99 37 48 56 7e 
    80 a1 a8 c8 06 78 27 6a  d2 01 3e b0 0a f3 ea 40 
    ba a2 fd bb db 0e fe 0b  ef fa dd 6e 97 5c de 51 
    7c bb ec 76 e5 f6 15 bd  84 50 88 ef 14 75 6a 44 
    23 e0 60 48 0b 77 c8 eb  3a 69 82 2f d4 5b 44 df 
    21 0d 03 cf e1 3e aa 87  6f 6e e8 fb 9f 9e 93 26 
    ab 22 ae 8c 62 26 0f 11  79 72 a5 76 9a bb 4b f5 
    ca 20 3d dc 97 14 81 4c  70 1d 2e 02 67 46 a7 4a 
    53 56 34 99 44 19 c0 8d  3d 74 9d 85 ef a2 74 04 
    b0 ca c1 5e cf 0c c4 35  81 60 6f ca 72 b7 2e 24 
    6e 41 97 47 38 3c d2 84  ca 6d ad 6b 06 2f a7 07 
    cd bd 69 b5 0a ce 16 ca  d5 aa e4 d3 73 7e bd d2 
    e8 32 9e f2 37 ad 89 c2  d0 65 4c e5 35 2d 51 d1 
    c2 77 56 96 db d0 39 b7  09 4b fe 39 14 f1 ef a8 
    d7 09 5e 4d 97 35 d6 15  f4 19 27 81 44 43 16 ea 
    af 56 69 3a 1b dc bf 2f  b0 80 a5 af 3c 75 ed f1 
    d4 35 a0 bd ed 29 d0 32  7d 69 1c 20 4a 1b 51 60 
    c5 30 95 a5 c6 fe 74 b9  34 f6 69 70 e0 b7 6f 3a 
    29 b9 dd bb 61 a7 40 6d  f7 ee 5d 27 4f 6c f7 ee 
    43 27 21 16 13 e2 7c 3a  b7 31 0a 76 c2 a3 2c ad 
    82 7a b2 b6 7e e4 98 9c  91 09 39 24 e7 99 77 2d 
    e1 f5 48 7a 63 a7 d7 19  29 b0 7e fc 87 bd d5 f9 
    5b bd 4b 5d 73 c3 98 d0  97 6c e2 19 b9 21 43 f2 
    8e 7c 20 6f 64 f3 6f 60  59 cc 7e b4 81 2e cb 26 
    56 8f 58 16 f9 8b fc 49  3e 91 b7 e4 6b 26 ea 27 
    b4 20 c1 e4 0b b5 ec 8e  d5 eb de 59 16 1e 63 7f 
    e6 af 30 1a 56 68 78 8e  ce 2b 74 56 a1 77 c9 93 
    a8 a8 b3 8a 3a 1e b0 1b  e4 81 6a e6 c3 de 89 f9 
    90 18 b3 ef f4 af ce 17  be 78 77 7f 76 3e 67 2b 
    77 f7 a9 73 9a 2e db dd  db ce 13 2f 00 d4 a3 a2 
    74 c9 3d f4 fb 9c f6 3b  2d f4 7b ca f7 fb 52 ec 
    b7 80 7e a7 69 bf a7 42  bf 2f f9 7e 9f 8b fd 9e 
    a0 df 53 da ef 4b a1 df  e7 7c bf d3 62 bf 2f f4 
    3b f0 ea 1e d8 b3 58 7d  a7 d2 d7 64 b2 4c b1 be 
    e6 27 cc 14 eb 6b 6e d2  a8 58 5f c5 bc e5 04 d0 
    bc 47 90 cf 05 90 d3 35  90 a7 12 c8 97 12 c8 02 
    41 4e 0b 20 4f 6b 20 5f  4a 20 9f 4b 20 4f 08 f2 
    54 00 f9 b2 06 f2 b9 04  72 5a 02 e1 42 45 bf 9b 
    89 38 d1 7b 33 11 24 ba  30 13 11 a2 4f d9 b6 63 
    0b f2 b6 23 61 b4 8b 67  23 1f e5 d2 6d a4 dc 65 
    24 0b ff f0 84 eb 60 a5  22 b2 17 28 58 2a 98 fa 
    f7 f7 d9 de 9b 4f c6 a0  71 1e 33 c3 c9 f6 20 a5 
    15 d0 01 17 62 5a d7 a9  40 5a c7 ab c7 9b ab 47 
    9b ab bd 42 b5 fc ec 52  0f 52 37 5f 19 2b a3 55 
    be 6c a6 14 0f f1 9e 5f  46 45 19 17 de 83 85 ef 
    b3 42 a5 92 05 d1 e1 20  5c 54 84 f3 1b b1 94 6a 
    2e 55 b6 2b ec 1a ad 27  ce 39 f6 f5 74 37 6e 20 
    79 78 7d 84 fb 98 5c 5d  0d ea b2 eb 4f 50 93 a7 
    c5 2b d1 e2 25 b4 78 9b  68 c9 0f dc 00 18 48 c6 
    c4 bd fa a4 7a bb 41 1a  f2 ea 61 e8 f9 6e 5a b7 
    87 4d 15 5a 99 55 92 e4  cf cb df 0a c3 fd 38 a4 
    08 17 07 28 88 f8 8f a0  2f 2a 31 5b d0 c3 48 89 
    04 fe 1f b4 81 39 4f 9e  61 78 56 c4 3f 8a dd d9 
    1a fb 1d a3 a0 64 f7 e3  d1 55 87 dc 55 8f 15 dc 
    98 e1 17 f3 02 c6 e1 80  52 3c 7b 53 a3 f0 01 d2 
    da 89 cc da 44 49 1b 3c  13 4a 6f e4 8d b3 e9 f4 
    92 7b 88 23 ea 84 36 fb  0b 2a c8 44 c2 b1 e7 9a 
    7c cc b1 52 a1 15 41 d4  28 bd 14 58 31 5f 54 14 
    3e ee 84 d2 6d 4d 7e ce  7f 15 1f fe a0 b5 1c 03 
    f8 3a 4f 14 aa 0b 11 29  8d 97 c3 ae 00 f9 4c 68 
    38 34 4a 93 f8 2b 25 1e  8a a4 37 e4 47 62 39 71 
    e7 60 a2 04 e9 45 4a 39  cd 10 7c 2e 1f 07 95 4a 
    db cf 66 6d 67 bb e5 0f  30 6c f8 a0 82 7b 62 27 
    72 fc cf af ec 10 17 77  38 9f 47 b3 76 e5 80 17 
    2a 6d f6 5b 59 59 49 9e  32 5d b8 71 6e ef 9c 6d 
    51 e2 9f 10 70 c6 e1 b5  b6 b3 89 35 c0 c3 55 c8 
    d6 62 9b c2 a8 4a a5 fd  ea 55 04 41 1b 2e 3d a6 
    7c af a2 7b 75 e0 f5 0f  16 de 60 86 5b 48 ce 02 
    c0 1c fa e0 f6 ac 28 aa  b2 3f 94 13 a3 55 67 ec 
    4d af 4e a7 10 ed cd a9  56 e5 e3 57 94 0d 7f 27 
    10 c3 48 d5 a1 33 85 af  11 94 a6 d3 d9 6c 71 0f 
    2f 3d a9 82 25 ae 93 42  64 e3 82 c8 56 ab bf 20 
    c9 31 e7 2a ee 4f 08 d1  4e 2a 22 51 e1 24 7a e9 
    70 f5 70 52                                      
    d9 d7 b4 e5 d2 4d be ba  05 ad fd c5 09 ad 84 bc 
    8a 4b 03 43 4c 08 47 0a  0d 71 4e 45 db 04 3f d9 
    b8 cd df 1e 67 94 66 66  ec 84 11 ff 74 44 6c 0b 
    88 d3 59 ef 87 0d 2a b5  d2 7f 15 de 63 4a 63 a5 
    22 f0 c5 16 6e 0a 01 8e  25 3d 52 9a 82 2b 78 86 
    f4 dd 13 5b 85 73 0a 06  27 db 26 93 49 c8 97 30 
    99 ee fc 1f 33 72 4c 37  b4 9a e7 86 4a 0e 8c f2 
    02 3d f9 ef 09 b4 f1 df  11 68 a8 74 a7 33 a8 19 
    63 69 8c df 5c 61 04 2a  6b 1f c4 df ec cc e4 ff 
    5f 5d ad 67 53 73 03 6b  02 fe 5f 99 a9 58 40 4a 
    7c d0 2b 56 81 05 9c d2  7f a2 cd bf b9 ee 2b 53 
    98 bd 1b 9c 9b d8 41 a4  16 8b 61 fe f7 7f fe 0d 
    c2 a2 d6 44 8e 3c 00 00  0d 0a 30 0d 0a 0d 0a    
";


            //string result = "";
            //using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(key1.Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", ""))))
            //{
            //    //using (DeflateStream zipStream = new DeflateStream(ms, CompressionMode.Decompress))
            //    //using (StreamReader sr = new StreamReader(zipStream, Encoding.Default))
            //    //    result = sr.ReadToEnd();

            //    using (GZipStream zipStream = new GZipStream(ms, CompressionMode.Decompress))
            //    using (StreamReader sr = new StreamReader(zipStream, Encoding.Default))
            //        result = sr.ReadToEnd();
            //}

            //return;



            var t = ConfigurationManager.AppSettings["ismoney"];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }


    //public class WxBox
    //{

    //    static String generateDeviceId()
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("e");
    //        int count = 15;//15个随机数字
    //        Random ran = new Random();
    //        for (int i = 0; i < count; i++)
    //        {
    //            int num = (int)(ran.Next(10));
    //            sb.Append(num);
    //        }

    //        return sb.ToString();
    //    }
    //    private String uuid; //用于标识当前登陆的唯一标记
    //    private String Skey;//登陆后与服务器进行交互的密钥
    //    private String wxuin;//用于cookie中，与服务器端交互的数据信息
    //    private String wxsid;//用于cookie中，与服务器端交互的数据信息
    //    private String deviceid = generateDeviceId();//"e157113007325620";//设置ID，根据官方的数据，应该是由e+随机数字生成的字符串
    //    private String synkey;//用于同步保持心跳的交互密钥

    //    private String token_login_url;//用于得到经过第三步骤后，返回的真正的登陆地址。
    //    private readonly String new_refer = "wx2.qq.com"; //目前的协议中，使用是最近的该域名
    //    private readonly String old_refer = "wx.qq.com";//在相应交互中，也有该域名的出现，但最终都会被重定向到新域名那里去

    //    private CookieContainer cookieStore; //用于保存PC端与服务器间交互时的cookie信息

    //    private SyncKey syncKey;//主要用于浏览器端与服务器端保持心跳的交互key 

    //    private UserInfo info;//

    //    //最初的登陆页面，主要用于通过该请求链接，得到一个唯一的uuid，并由该UUID在全局使用。
    //    //设为Step 1.
    //    //请求方法 GET，返回值uuid
    //    private String jsLogin_url = "https://login.weixin.qq.com/jslogin?appid=wx782c26e4c19acffb&redirect_uri=https%3A%2F%2Fwx.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage&fun=new&lang=zh_CN"
    //            + "&_={time}";
    //    //根据Step1所得到的uuid，去访问并获取对应的二维码图片，用于手机扫描。
    //    //--不得不吐槽竟然让用户用手机去扫描，我要是用手机上，还需要用网页版的干嘛？方便打字？
    //    //Step 2 
    //    //请求方法 GET 
    //    //https://login.weixin.qq.com/qrcode/Adu64_96dg==
    //    private String qr_code_url = "https://login.weixin.qq.com/qrcode/{uuid}";

    //    //Step 3
    //    //由于在step2中已经提供了二维码给手机端进行扫描，那就意味着必须有另一个动作去轮询服务器，以确定手机是否已经扫描到，并同意用户在网页端登陆微信
    //    //请求方法 GET 
    //    //如果返回的响应中包括有window.redirect_uri=xxxxxx(URL地址)。
    //    //即表示手机端已经同意授权,而此时响应中提供的用于跳转的URL，则是用于让用户去获取相应cookie的关键步骤
    //    //但是，很遗憾的告诉你，该URL中的域名是以wx.qq.com开头的，由于目前微信协议中的新的refer已经改为wx2.qq.com了，
    //    //所以，实际上你应该访问的登陆地址由   https://wx.qq.com/xxxxxxx   改为 https://wx2.qq.com/xxxxxxx 才对。
    //    //访问该地址后，从返回的cookie中至少会得到 wxuin和wxsid 这二个信息。 这二个信息的重要性，主要用于之后与服务器端进行交互认证之用途。
    //    //返回地址如下：https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxnewloginpage?ticket=d85eb3c260c14b5ea2959a1e783b7681&lang=zh_CN&scan=1397376800
    //    private String checkAuth_url = "https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?uuid={uuid}&tip=1&_={time}";

    //    //Step 4
    //    //请求方法 POST，content-type类型为application/json; charset=utf-8 
    //    //需要提供之前保存的cookie信息以及如下的json信息内容：
    //    /*
    //     {"BaseRequest":{"Uin":"2545437902","Sid":"QfLp+Z+FePzvOFoG","Skey":"","DeviceID":"e1615250492"}}
    //     该方法会返回手机微信主页上的信息，其中包括小部分的联系人的方式等等。
    //     可以通过WebWxInitBean来装载其中的数据 
    //     */
    //    private String web_wx_init_url = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r={time}&skey=";

    //    //Step 5
    //    //通过浏览器端的监控，发现在第四步之后，并不是直接调用获取全部的用户列表，而是紧接着调用 如下URL，从URL的命名来看，应该是在第四步成功之后，
    //    //再次向服务器端提供的一次验证，而且这次验证在返回的json中的syncKey将会做为此后心跳机制中的交互码。在第4步中也会返回此码，二次得到的syncKey并不一样
    //    //估计是为了迷惑别人，故意如此做吧。但是在该此请求中，却必须在post体中带上第4步中得到syncKey。
    //    //另外从该资源的返回内容来看，该请求应该也包括了获取用户消息的请求。
    //    //形如"https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid={sid}&r={time}&skey={skey}";
    //    private String web_wx_sync = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsync";

    //    //Step 6
    //    //根据观察，发现这个是一个自己给自己发送信息的一个状态包，估计是用来保持网页端与手机端之间状态的一个方式吧。
    //    //方法POST
    //    //"https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxstatusnotify?r=1397443950116&skey=%40crypt_cfbfba84_e5913dbec2b764d086b7d1d1aab946ca";
    //    private String web_wx_status_notify = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxstatusnotify";

    //    //Step 7
    //    /*
    //     该URL用于定时性的向服务器发送心跳包，在返回值中的，有相应的状态码，大致如下window.synccheck={retcode:”0″,selector:”0″}
    //     其中，当selector不等于0即，意味着有消息需要客户端发起请求。
    //     请求的方法类型为 GET
    //     */
    //    private String sync_check_url = "https://webpush2.weixin.qq.com/cgi-bin/mmwebwx-bin/synccheck?"
    //            + "skey={skey}"
    //            + "&callback=jQuery183008612365838727565_1397377003545"
    //            + "&r={r}&sid={sid}&uin={uin}&deviceid={deviceid}"
    //            + "&synckey={synckey}&_={time}";

    //    private QrCodeFrame frame;//用来显示二维码窗口，如果用户直接关闭，程序则直接退出。

    //    private int login_try_times = 50;//用来检测用户扫描二维码时的，尝试次数，如果用户在特定的次数里，服务器端没有能返回授权通过的信息，则认为登录失败。

    //    private WebWxInitBean webWxInitBean;

    //    //private LoginCallback loginCallback = new DefaultLoginCallback();

    //    public WxBox(Form1 frm)
    //    {
    //        this.cookieStore = new CookieContainer();
    //        this.syncKey = new SyncKey();
    //        this.FORM = frm;
    //    }

    //    public void login()
    //    {
    //        try
    //        {
    //            GetUrl("https://wx.qq.com/?&lang=zh_CN");


    //            this.uuid = generateUUID();
    //            if (uuid != null)
    //            {
    //                loadQrCodeImage();
    //                //loginCallback.handleResult(frame, loadUriToken());

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //    }

    //    //    public void configWxData() {

    //    //    cookieStore = new BasicCookieStore();//从真实的登陆链接处，装载相应的cookie信息

    //    //    CloseableHttpClient client = HttpClients.custom().setDefaultCookieStore(cookieStore).build();

    //    //    HttpGet httpGet = new HttpGet(this.token_login_url);
    //    //    System.out.println("request line:" + httpGet.getRequestLine());
    //    //    try {
    //    //        // 执行get请求  
    //    //        HttpResponse httpResponse = client.execute(httpGet);
    //    //        System.out.println("cookie store:" + cookieStore.getCookies());
    //    //        //for (Cookie c : cookieStore.getCookies()) {
    //    //        //    if ("wxsid".equals(c.getName())) {
    //    //        //        this.wxsid = c.getValue();
    //    //        //        System.out.println(wxsid);
    //    //        //    } else if ("wxuin".equals(c.getName())) {
    //    //        //        this.wxuin = c.getValue();
    //    //        //        System.out.println(wxuin);
    //    //        //    }
    //    //        //}
    //    //    } catch (IOException e) {
    //    //        e.printStackTrace();
    //    //    } finally {
    //    //        try {
    //    //            // 关闭流并释放资源  
    //    //            client.close();
    //    //        } catch (IOException e) {
    //    //            e.printStackTrace();
    //    //        }
    //    //    }
    //    //}

    //    //    public void configWebWxInitBean()
    //    //    {

    //    //        HttpPost post = new HttpPost(this.web_wx_init_url.replace("{time}", String.valueOf(System.currentTimeMillis())));
    //    //        BaseRequest request = new BaseRequest(this.wxuin, this.wxsid, "", this.deviceid);
    //    //        WebWxInitJson json = new WebWxInitJson(request);
    //    //        Gson gson = new Gson();
    //    //        StringEntity se = new StringEntity(gson.toJson(json));
    //    //        se.setContentEncoding(new BasicHeader(HTTP.CONTENT_TYPE, "application/json; charset=utf-8"));
    //    //        post.setEntity(se); //post方法中，加入json数据

    //    //        if (cookieStore == null)
    //    //        {
    //    //            System.err.print("Cookies 信息丢失!");
    //    //            System.exit(-1);
    //    //        }

    //    //        CloseableHttpClient httpclient = HttpClients.custom().setDefaultCookieStore(cookieStore).build();//将cookie信息添加到请求中

    //    //        HttpResponse response = httpclient.execute(post);

    //    //        StringBuilder rs = new StringBuilder();
    //    //        if (HttpStatus.SC_OK == response.getStatusLine().getStatusCode())
    //    //        {
    //    //            HttpEntity entity = response.getEntity();
    //    //            InputStreamReader insr = new InputStreamReader(entity.getContent());
    //    //            int respInt = insr.read();

    //    //            while (respInt != -1)
    //    //            {
    //    //                rs.append((char)respInt);
    //    //                respInt = insr.read();

    //    //            }
    //    //        }
    //    //        this.webWxInitBean = gson.fromJson(rs.toString(), WebWxInitBean);
    //    //        this.syncKey = webWxInitBean.getSyncKey();
    //    //        this.Skey = webWxInitBean.getSkey();
    //    //        this.info = webWxInitBean.getUser();
    //    //        print(info);
    //    //    }

    //    //    public WebWxSyncPostBean loadMsgAndUpdateSyncKey() {
    //    //    WebWxSyncPostJson post_json = new WebWxSyncPostJson(this.wxsid, this.wxuin);
    //    //    post_json.setRr(System.currentTimeMillis());
    //    //    post_json.setSynKey(this.syncKey);
    //    //    Gson gson = new Gson();
    //    //    CloseableHttpClient httpclient = HttpClients.custom().setDefaultCookieStore(cookieStore).build();//将cookie信息添加到请求中
    //    //    //private String web_wx_sync = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid={sid}&r={time}&skey={skey}";
    //    //    HttpPost post = new HttpPost(web_wx_sync);
    //    //    List<NameValuePair> params = new ArrayList<>();
    //    //    params.add(new BasicNameValuePair("sid", this.wxsid));
    //    //    params.add(new BasicNameValuePair("r", String.valueOf(System.currentTimeMillis())));
    //    //    params.add(new BasicNameValuePair("skey", Skey));
    //    //    post.setEntity(new UrlEncodedFormEntity(params, "UTF-8"));

    //    //    StringEntity se = new StringEntity(gson.toJson(post_json));

    //    //    se.setContentEncoding(new BasicHeader(HTTP.CONTENT_TYPE, "application/json; charset=utf-8"));

    //    //    post.setEntity(se); //post方法中，加入json数据

    //    //    HttpResponse response = httpclient.execute(post);
    //    //    StringBuilder rs = new StringBuilder();
    //    //    if (HttpStatus.SC_OK == response.getStatusLine().getStatusCode()) {
    //    //        HttpEntity entity = response.getEntity();
    //    //        InputStreamReader insr = new InputStreamReader(entity.getContent());
    //    //        int respInt = insr.read();
    //    //        while (respInt != -1) {
    //    //            rs.append((char) respInt);
    //    //            respInt = insr.read();
    //    //        }
    //    //    }

    //    //    WebWxSyncPostBean bean = gson.fromJson(rs.toString(), WebWxSyncPostBean);
    //    //    if (bean.getSyncKey().getCount() != 0) {
    //    //        this.syncKey = bean.getSyncKey();
    //    //    }
    //    //    return bean;
    //    //}

    //    //    /**
    //    //     * 同时同步手机及网页端的通讯状态
    //    //     */
    //    //    public boolean wxStatusNotify(){
    //    //    Gson gson = new Gson();

    //    //    WebWxStatusNotifyJson json = new WebWxStatusNotifyJson(this.wxuin, this.wxsid, this.Skey, this.deviceid);

    //    //    String msgid = String.valueOf(System.currentTimeMillis());
    //    //    String timestamp = msgid;

    //    //    json.setClientMsgId(msgid);
    //    //    json.setFromUserName(info.getUserName());
    //    //    json.setToUserName(info.getUserName());

    //    //    CloseableHttpClient httpclient = HttpClients.custom().setDefaultCookieStore(cookieStore).build();//将cookie信息添加到请求中

    //    //    HttpPost post = new HttpPost(web_wx_status_notify);
    //    //    List<NameValuePair> params = new ArrayList<>();
    //    //    params.add(new BasicNameValuePair("r", timestamp));
    //    //    params.add(new BasicNameValuePair("skey", Skey));
    //    //    post.setEntity(new UrlEncodedFormEntity(params, "UTF-8"));//设置post参数

    //    //    StringEntity se = new StringEntity(gson.toJson(json));
    //    //    se.setContentEncoding(new BasicHeader(HTTP.CONTENT_TYPE, "application/json; charset=utf-8"));
    //    //    post.setEntity(se); //post方法中，加入json数据

    //    //    HttpResponse response = httpclient.execute(post);
    //    //    StringBuilder rs = new StringBuilder();
    //    //    if (HttpStatus.SC_OK == response.getStatusLine().getStatusCode()) {
    //    //        HttpEntity entity = response.getEntity();
    //    //        InputStreamReader insr = new InputStreamReader(entity.getContent());
    //    //        int respInt = insr.read();
    //    //        while (respInt != -1) {
    //    //            rs.append((char) respInt);
    //    //            respInt = insr.read();
    //    //        }
    //    //    }
    //    //    print(rs.toString());

    //    //    WebWxStatusNotifyBean bean = gson.fromJson(rs.toString(), WebWxStatusNotifyBean);

    //    //    print(bean);
    //    //    return bean.getBr().getRet() == 0;

    //    //}

    //    //    /**
    //    //     * 用于定时向服务器发送心跳包，同时，根据心跳包中的返回信息，确认是否需要发请另外的请求，去获取消息。
    //    //     */
    //    //    public HeartBeatBean syncCheck()
    //    //    {

    //    //        String url = sync_check_url.replace("{skey}", URLEncoder.encode(this.Skey, "utf-8")).replace("{r}", String.valueOf(System.currentTimeMillis()))
    //    //                .replace("{sid}", URLEncoder.encode(this.wxsid, "utf-8")).replace("{uin}", this.wxuin).replace("{deviceid}", this.deviceid)
    //    //                .replace("{synckey}", URLEncoder.encode(this.contactSyncKey(syncKey), "utf-8")).replace("{time}", String.valueOf(System.currentTimeMillis()));

    //    //        CloseableHttpClient httpclient = HttpClients.custom().setDefaultCookieStore(cookieStore).build();//将cookie信息添加到请求中

    //    //        HttpGet get = new HttpGet(url);

    //    //        HttpResponse response = httpclient.execute(get);
    //    //        StringBuilder rs = new StringBuilder();
    //    //        if (HttpStatus.SC_OK == response.getStatusLine().getStatusCode())
    //    //        {
    //    //            HttpEntity entity = response.getEntity();
    //    //            InputStreamReader insr = new InputStreamReader(entity.getContent());
    //    //            int respInt = insr.read();
    //    //            while (respInt != -1)
    //    //            {
    //    //                rs.append((char)respInt);
    //    //                respInt = insr.read();
    //    //            }
    //    //        }

    //    //        return HeartBeatBean.parseString(rs.toString());
    //    //    }

    //    //    private String loadUriToken()
    //    //    {

    //    //        this.token_login_url = null;
    //    //        int index = 0;
    //    //        while (token_login_url == null && index < this.login_try_times)
    //    //        {
    //    //            // 创建URL对象

    //    //            String rs = loadResultFromHttp(checkAuth_url.replace("{uuid}", uuid).replace("{time}", String.valueOf(System.currentTimeMillis())));

    //    //            if (rs.indexOf("200") == -1)
    //    //            {
    //    //                Thread.sleep(5000);
    //    //            }
    //    //            else
    //    //            {
    //    //                Pattern p = Pattern.compile("\"(.*?)\"");
    //    //                Matcher m = p.matcher(rs);
    //    //                while (m.find())
    //    //                {
    //    //                    rs = m.group();
    //    //                    token_login_url = rs.substring(1, rs.length() - 1);
    //    //                    print("获取到的原始登陆地址：" + token_login_url);
    //    //                    break;
    //    //                }
    //    //            }
    //    //            index++;
    //    //        }
    //    //        token_login_url = token_login_url.replace(this.old_refer, this.new_refer);
    //    //        print("下转换得到的正确的登陆地址：" + token_login_url);
    //    //        return token_login_url;

    //    //    }

    //    //    private String loadResultFromHttp(String url)
    //    //    {
    //    //        StringBuilder sb = new StringBuilder();
    //    //        CloseableHttpClient httpclient = HttpClients.createDefault();

    //    //        HttpGet httpget = new HttpGet(url);
    //    //        //try
    //    //        {
    //    //            //(CloseableHttpResponse response1 = httpclient.execute(httpget)) 
    //    //            HttpEntity entity = response1.getEntity();
    //    //            InputStreamReader insr = new InputStreamReader(entity.getContent());
    //    //            int respInt = insr.read();

    //    //            while (respInt != -1)
    //    //            {
    //    //                sb.append((char)respInt);
    //    //                respInt = insr.read();
    //    //            }
    //    //            EntityUtils.consume(entity);

    //    //        }

    //    //        return sb.toString();
    //    //    }

    //    //    private void print(Object obj) {
    //    //    //System.out.println(obj);
    //    //    }

    //    private void loadQrCodeImage()
    //    {

    //        //BufferedImage image = ImageIO.read(Request.Get(this.qr_code_url.replace("{uuid}", uuid))

    //        var t = GetUrlStream(this.qr_code_url.Replace("{uuid}", uuid));

    //        FORM.pictureBox1.Image = Image.FromStream(t);
            
    //        //        .connectTimeout(10000)
    //        //        .socketTimeout(10000)
    //        //        .execute().returnContent().asStream());

    //        //if (image != null)
    //        //{
    //        //    frame = new QrCodeFrame();
    //        //    frame.getQr_label().setSize(image.getWidth() + 10, image.getHeight() + 10);
    //        //    frame.getQr_label().setIcon(new ImageIcon(image));
    //        //    frame.setAlwaysOnTop(true);
    //        //    frame.pack();
    //        //    frame.setVisible(true);
    //        //}
    //    }

    //    // DateTime时间格式转换为Unix时间戳格式
    //    public int DateTimeToStamp(System.DateTime time)
    //    {
    //        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
    //        return (int)(time - startTime).TotalSeconds;
    //    }

    //    public Stream GetUrlStream(string url, bool ispost = false, string bianma = "utf-8", string content = "")
    //    {

    //        Uri ourUri = new Uri(url);
    //        HttpWebRequest web = (HttpWebRequest)WebRequest.Create(ourUri);
    //        web.ReadWriteTimeout = 100;
    //        web.CookieContainer = this.cookieStore;
    //        if (ispost)
    //        {
    //            web.Method = "Post";
    //        }

    //        Encoding en = Encoding.GetEncoding(bianma);

    //        if (content.Length > 0)
    //        {
    //            Stream myRequestStream = web.GetRequestStream();
    //            var byt = en.GetBytes(content);
    //            myRequestStream.Write(byt, 0, byt.Length);
    //            myRequestStream.Close();
    //        }

    //        WebResponse myWebResponse = (HttpWebResponse)web.GetResponse();

    //        //foreach(Cookie c in myWebResponse.Cookies)


    //        return myWebResponse.GetResponseStream();
    //        //using (WebClient wc = new WebClient())
    //        //{
    //        //    var bytes = wc.DownloadData(url);
    //        //    return new MemoryStream(bytes);
    //        //}

    //    }

    //    public string GetUrl(string url, bool ispost = false, string bianma = "utf-8", string content = "")
    //    {
    //        try
    //        {
    //            Uri ourUri = new Uri(url);
    //            HttpWebRequest web = (HttpWebRequest)WebRequest.Create(ourUri);
    //            web.ReadWriteTimeout = 100;
    //            web.CookieContainer = this.cookieStore;
    //            if (ispost)
    //            {
    //                web.Method = "Post";
    //            }

    //            Encoding en = Encoding.GetEncoding(bianma);

    //            if (content.Length > 0)
    //            {
    //                Stream myRequestStream = web.GetRequestStream();
    //                var byt = en.GetBytes(content);
    //                myRequestStream.Write(byt, 0, byt.Length);
    //                myRequestStream.Close();
    //            }

    //            WebResponse myWebResponse = (HttpWebResponse)web.GetResponse();

    //            StreamReader stream = new StreamReader(myWebResponse.GetResponseStream(), en);

    //            return stream.ReadToEnd();
    //        }
    //        catch (Exception ex)
    //        {
    //            return "";
    //        }

    //    }

    //    private String generateUUID()
    //    {

    //        String rs = GetUrl(jsLogin_url.Replace("{time}", DateTimeToStamp(DateTime.Now).ToString()));
    //        if (rs.IndexOf("200") != -1)
    //        {
    //            Regex reg = new Regex("\"(.*?)\"");
    //            Match m = reg.Match(rs);
    //            while (m.Success)
    //            {
    //                rs = m.Groups[1].Value;
    //                return rs.Substring(1, rs.Length - 1);
    //            }
    //        }
    //        return null;

    //    }

    //    ///**
    //    // * @desc 由于在url中的SyncKey都是通过key_value|key_value...等形式进行连接的，所以，对参数进行编码
    //    // * @param sk
    //    // * @return 编码后的字符串
    //    // */
    //    //private String contactSyncKey(SyncKey sk)
    //    //{
    //    //    StringBuilder sb = new StringBuilder();
    //    //    List<KeyVal> list = sk.getKeyList();
    //    //    int size = list.size();
    //    //    if (size == 0)
    //    //    {
    //    //        return "";
    //    //    }

    //    //    for (int index = 0; index < size; index++)
    //    //    {
    //    //        KeyVal kv = list.get(index);
    //    //        sb.append(kv.getKey()).append("_").append(kv.getVal());
    //    //        if (index != size - 1)
    //    //        {
    //    //            sb.append("|");
    //    //        }
    //    //    }
    //    //    return sb.toString();
    //    //}

    //    //public LoginCallback getLoginCallback()
    //    //{
    //    //    return loginCallback;
    //    //}

    //    //public void setLoginCallback(LoginCallback loginCallback)
    //    //{
    //    //    this.loginCallback = loginCallback;
    //    //}

    //    //    class DefaultLoginCallback implements LoginCallback {

    //    //        public void handleResult(Object target, String rs) {
    //    //            if (rs != null) {
    //    //                ((JFrame) target).setVisible(false);
    //    //            } else {
    //    //                JOptionPane.showMessageDialog(frame, "二维码信息过期，请重新尝试！", "登陆失败", JOptionPane.ERROR);
    //    //                System.exit(-1);
    //    //            }
    //    //        }
    //    //    }


    //    public Form1 FORM { get; set; }
    //}
}