# Time Calculator

Time Calculator adalah aplikasi desktop Windows sederhana untuk menghitung durasi waktu dalam menit dari waktu mulai hingga waktu selesai.

## Fitur

- Input waktu format 24 jam: `HH:mm`
- Pengguna cukup mengetik jam dan menit; tanda titik dua otomatis muncul
- Mendukung durasi yang melewati tengah malam
- Validasi input yang jelas dan aman
- Hasil utama dalam satuan menit
- Tombol `Calculate` dan `Clear`
- Shortcut keyboard: `Enter` untuk menghitung, `Escape` untuk membersihkan form

## Requirement

- .NET SDK 8.0 atau versi yang lebih baru
- Windows OS untuk menjalankan aplikasi WinForms
- VS Code + C# Dev Kit (disarankan)

## Menjalankan di VS Code

1. Buka folder project di VS Code.
2. Pastikan extension C# Dev Kit dan .NET SDK sudah terinstall.
3. Buka terminal di VS Code.
4. Jalankan:

```bash
dotnet restore
dotnet run
```

## Menjalankan dengan dotnet run

```bash
dotnet run
```

## Build project

```bash
dotnet build
```

## Publish menjadi executable Windows

Untuk menghasilkan file `.exe` di Windows x64:

```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

Untuk publish satu-file executable:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

Output biasanya berada di folder:

```text
bin/Release/net8.0-windows/win-x64/publish/
```

## Contoh penggunaan

- Start: `08:30`
- Stop: `11:45`
- Hasil: `195 minutes`

- Start: `23:30`
- Stop: `01:15`
- Hasil: `105 minutes`

## Struktur project

```text
TimeCalculator/
├── TimeCalculator.csproj
├── Program.cs
├── MainForm.cs
├── TimeCalculator.cs
├── README.md
```

## Catatan

Project ini mengikuti struktur WinForms yang sederhana dan kompatibel dengan `dotnet` CLI. Semua logika perhitungan dipisahkan ke kelas `TimeCalculator`, sementara form hanya menangani input, validasi, dan tampilan hasil.
