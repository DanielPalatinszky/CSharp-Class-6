using System;
using System.Collections.Generic;
using System.IO;

namespace CSharp_Class_6
{
    class Program
    {
        static void Main(string[] args)
        {
            // Kétféle hiba lehet: szintaktikai és szemantikai
            // Szintaktika: nyelvtani hiba (pl. lefelejtjük a pontosvesszőt)
            // Szemantikai: logikai hiba
            // A szemantikai hibákat is szétválaszthatjuk két alesetre
            // 1. Azok a szemantikai hibák, melyek miatt nem azt csinálja a szoftver amit szeretnénk (pl. egész/egész osztás, pedig törtet várunk)
            // 2. Azok a szemantikai hibák, melyek konkrét hibát okoznak, azaz elszáll a program (pl. tömb túlindexelése)

            // Most a 2. kategóriával foglalkozunk. Mégpedig, hogy mit tehetünk ellene?
            // 1. Megelőzzük a hibát (pl. ellenőrizzük, hogy a fájl amit meg akarunk nyitni létezik-e)
            // 2. Megfogjuk a hibát -> kivételkezelés (ezzel foglalkozunk)

            // Vegyünk egy egyszerű hibát: nullával való osztás
            // var a = 0;
            // var b = 1 / a; // kivételt (hibát) okoz -> DivideByZeroException (exception = kivétel) -> elszáll a program

            // Kapjuk el:
            // try-ba kerül az a kód, amely kivételt okozhat
            // catch-be kerül az a kód, amely akkor fut le, amikor elkapjuk a hibát
            // A program nem száll el, mivel elkaptuk a kivételt!
            try
            {
                var c = 0;
                var d = 1 / c;
            }
            catch (Exception e) // Mindenféle kivételt elkapunk
            {
                Console.WriteLine("Hiba keletkezett!");
            }

            // Többféle kivételt is elkaphatunk:
            try
            {
                var e = 0;
                var f = 1 / e;
            }
            catch (DivideByZeroException e) // Csak akkor fut le, ha nullával való osztás történt
            {
                Console.WriteLine("Megpróbáltál nullával osztani!");

                // Hibaüzenet kiírása:
                Console.WriteLine(e.Message);

                // Hiba helyének kiírása:
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception e) // Minden más hibára lefut, ami nem nullával való osztás, mivel azt az imént kezeltük
            {
                Console.WriteLine("Ismeretlen hiba!");
            }

            // Tehát egy try-hoz tetszőleges számú catch tartozhat:
            try
            {
                var t = new int[2];
                t[3] = 0;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Túlindexelted a tömböt!");
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Megpróbáltál nullával osztani!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ismeretlen hiba!");
            }

            // A catch ágak mindig fentről lefele futnak és az az ág fog először lefutni, amelyre először passzol a keletkező kivétel

            // A catch ágakat fentről lefele a legkonkrétabbtól a legáltalánosabbig kell felsorolnunk
            // Nem fordul le:
            /*try
            {

            }
            catch (Exception e)
            {
            }
            catch (DivideByZeroException e)
            {

            }*/

            // El is hagyhatjuk a catch utáni zárójelet, ha nem érdekelnek minket a keletkező kivétel részletei
            try
            {

            }
            catch
            {
            }

            // Ha lehet használjuk a beépített kivételeket: DivideByZeroException, NullReferenceException, IndexOutOfRangeException, FileNotFoundException stb.
            // Ha üresen hagyjuk a catch ágat, mert nem szeretnünk az elkapott kivétellel semmit sem kezdeni, akkor kommenteljük oda, hogy direkt hagytuk üresen

            // Előfrodulhat, hogy van olyan kódrészlet, amit akkor is le akarunk futtatni amikor keletkezett hiba és akkor is amikor nem (pl. fájl bezárása)
            // Megoldás: opcionálisan a try-catch után szerepelhet 1 db finally ág
            try
            {

            }
            catch (Exception e)
            {
            }
            finally
            {
                // Az itt levő kód akkor is lefut, ha keletkezett hiba és akkor is ha nem
            }

            // try szerepelhet csak finally-vel is -> ha hiba keletkezik, akkor nincs mi elkapja!
            try
            {
                var i = 0;
                var j = 1 / i;
            }
            finally
            {

            }

            // Természetesen mind a try, mind a catch, mind a finally ágon belül bármilyen kódot írhatunk
            // A try ág lehet hosszabb (ne vigyük túlzásba), de a catch és finally legyen minél rövidebb és egyszerűbb!

            // Ha elkaphatunk hibákat, akkor felmerül a kérdés, hogy tudunk-e mi magunk létrehozni?
            // Igen! A throw segítségével:
            // throw new Exception("Ez egy hiba");
            // throw new ArgumentException("Rossz paraméter!"); (használjuk a beépítetteket!)

            // Természetesen az általunk dobott hibákat is elkaphatjuk:
            try
            {
                throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // A kivételek végiggyűrűznek a metódushívásokon:
            try
            {
                Method1();
            }
            catch (Exception e)
            {
            }

            // Mindegy, hogy hol kapjuk el, azaz a hívási láncban bárhol megtehetjük, de ha kijut a Main metódusból, akkor a program elszáll!

            // Tovább is dobhatjuk a kivételt:
            /*try
            {
                throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw; // Tovább dob! (opcionálisan: throw e;)
            }*/

            // Felmerülhet a kérdés: akkor most mindenhova tegyek try-catch-et?
            // Egyáltalán nem! Mindig próbáljuk megelőzni a hibát külön ellenőrzésekkel (pl. if-ek). Ha ez nem jön be, akkor használjunk kivételkezelést, de akkor is csak az allábiaknak megfelelően!
            // Egyrészt a kivételek lassúak, másrészt csak akkor használjuk, ha a hibát értelmesen le tudjuk kezelni! Azaz a keletkező hiba után a programot vissza tudjuk állítani egy értelmes állapotba!
            // Ha nem tudjuk a hiba után értelmes állapotba hozni a programot, akkor minden bizonnyal egy olyan hibáról van szó, aminek nem szabadott volna megtörténnie. Azaz ki kell javítanunk.

            // Mikor használjunk throwt?
            // Ahelyett, hogy mindenféle rejtélyes hibakódokkal térnénk vissza a metódusainkból, inkább dobjunk kivételt!
            // Például a metódusnak megadott paraméter az értelmezési tartományon kívül esik -> pl. azt várjuk, hogy a paraméter nagyobb mint nulla, ha kisebb akkor dobjunk kivételt

            //--------------------------------------------------

            // Sokféle módja van annak, hogy fájlt kezeljünk (pl. File, StreamReader, StreamWriter, FileWriter)
            // Ami kell: using System.IO;

            // Legegyszerűbb módja: File
            File.AppendAllText("test.txt", "Hello File!"); // Beleírja a megadott fájlba a megadott szöveget (ha nem létezik a fájl, akkor létrehozza)
            File.AppendAllLines("test.txt", new List<string> { "A", "B", "C" }); // Mint az előző, csak kollekcióval dolgozik és minden elem után új sort rak

            // . és .. magyarázata:
            File.AppendAllText("./text", "A"); // . = aktuális mappa
            File.AppendAllText("../text", "B"); // .. = egy mappával feljebb

            // Továbbiak:
            File.Copy("test.txt", "test_copy.txt"); // Fájl másolás
            File.Delete("test_copy.txt"); // Fájl törlés
            File.Exists("file_copy.txt"); // Létezik-e a fájl
            var fajlTeljesTartalma = File.ReadAllText("test.txt");
            var fajlTeljesTartalmaSoronkent = File.ReadAllLines("test.txt");
            // stb. (File.Move, File.Open, File.Create, File.OpenRead, File.OpenText...)
            // Rengeteg adatot is le lehet kérdezni a fájlról (pl. teljes elérési útvonal, utolsó módosítás dátuma stb.), nézzünk körül!

            // Második fájlkezelési módszer: streamek
            // Mi a stream: egy adatfolyam, amiből ha egyszer valamit kiolvastunk, akkor (általában) már nem tudunk visszamenni.
            // Azaz folyamatosan olvasunk belőle, vagy folyamatosan írunk bele!

            // Olvasás:
            var streamReader = new StreamReader("test.txt");

            var sor = streamReader.ReadLine(); // Egy sor olvasása
            sor = streamReader.ReadLine(); // Következő sor olvasása

            var teljesTartalom = streamReader.ReadToEnd(); // Teljes tartalom kiolvasása (ami maradt)

            streamReader.Close(); // Streameket mindig be kell zárni!!! Ha nem tesszük meg és futva marad a program, akkor nem tudjuk törölni vagy módosítani a fájlt a programon kívülről, akkor sem ha a program maga már nem használja!

            // Fájl teljes olvasása és a bezárás biztosítása:
            StreamReader fajl = null;
            try
            {
                if (File.Exists("test.txt"))
                fajl = new StreamReader("test.txt");

                while (!fajl.EndOfStream) // Amíg nincs vége a fájlnak (stream-nek)
                {
                    sor = fajl.ReadLine();
                }
            }
            catch
            {
                Console.WriteLine("Hiba keletkezett!");
            }
            finally
            {
                // Mivel finally-n belül vagyunk, ezért ha keletkezett hiba, ha nem, bezárjuk a fájlt!
                fajl.Close();
            }

            // Írás:
            var streamWriter = new StreamWriter("test.txt");
            // var streamWriter = new StreamWriter("test.txt", true); // Megnyitás hozzáfúzésre
            try
            {
                // Mint konzolnál:
                streamWriter.WriteLine("Hello");
                streamWriter.Write("Fájl!");

                // A fájlok írása költséges művelet, ezért általában nem rögtön fájlba szoktunk írni, hanem egy ideiglenes tárba (buffer), aminek a tartalmát néha ürítjük, azaz beleírjuk az aktuális fájlba!
                // A StreamWriter is ilyen
                // Kapcsoljuk ki a buffer automatikus ürítését:
                streamWriter.AutoFlush = false;
                streamWriter.Write("A"); // Mivel kikapcsoltuk az ürítést, ezért nem fog megjelenni a fájlban!
                streamWriter.Flush(); // Kézzel ürítjük a buffert, azaz kiírjuk a fájlba a tartalmát, így már megjelenik a fájlban az iménti 'A' betű!

                // Ha az AutoFlush be van kapcsolva, akkor minden Write hívás után automatikusan ürül a buffer, a WriteLine után viszont nem!
                // Ezért ne lepődjünk meg, hogyha a WriteLine után nem látjuk a fájlban azonnal a beleírt szöveget.

                // A fájl bezárása automatikusan üríti a buffert a fájlba!

                // Hagyjuk bekapcsolva (alapértelmezett):
                streamWriter.AutoFlush = true;
            }
            catch
            {
                Console.WriteLine("Hiba keletkezett!");
            }
            finally
            {
                streamWriter.Close();
            }

            // A különböző erőforrások (pl. streamek) nyitása és zárása annyira gyakori művelet, hogy a C# külön rendszert tartalmaz ezek kezelésére:
            using (var test = new StreamReader("test.txt"))
            {
                test.ReadLine();
            }

            // A using biztosítja azt, hogy a using utáni zárójelben megadott erőforrás minden esetben be legyen zárva, ha kilépünk a using blokkjából! Akár keletkezett hiba, akár nem! (elkapni nem fogja a kivételt!)

            // Egyszerre tetszőleges számú erőforrás meg lehet nyitva, mindegyik be fogja zárni, ha kilpünk a blokkjából:
            using (var fajl1 = new StreamWriter("test_copy.txt"))
            using (var fajl2 = new StreamReader("test.txt"))
            {
                while (!fajl2.EndOfStream)
                    fajl1.WriteLine(fajl2.ReadLine());
            }

            // Sokféle stream létezik! (pl. MemoryStream)
            // Sajátot is készíthetünk!

            //--------------------------------------------------

            // Van még két fontosabb fájl és mappa kezelő típus: FileInfo és DirectoryInfo:

            // A FileInfo segítségével adott fájlról kérdezhetünk le mindenféle adatot (vagy kezelhetjük azt):
            var fileInfo = new FileInfo("test.txt");

            var dir = fileInfo.Directory; // Melyik mappában van
            var exists = fileInfo.Exists; // Létezik-e
            var reader = fileInfo.OpenText(); // Olvasó nyitása
            reader.Close();
            var writer = fileInfo.CreateText(); // Író nyitása
            writer.Close();
            // stb.

            var directoryInfo = new DirectoryInfo("./Mappa"); // Aktuális mappában levő "Mappa" nevű mappa
            var dirExists = directoryInfo.Exists; // Létezik-e
            var path = directoryInfo.FullName; // Teljes elérhetőség
            directoryInfo.Create(); // Mappa létrehozása
            directoryInfo.Delete(); // Mappa törlése
            // stb.
        }

        static void Method1()
        {
            Method2();
        }

        static void Method2()
        {
            throw new Exception();
        }
    }
}
