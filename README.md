# Block Blast (Uyin-123)

Unity 6 da yozilgan Android uchun blok-puzzle o'yini. Shakllarni 8×8 taxtaga sudrab qo'yasiz, to'lgan qator va ustunlar tozalanadi, joy qolmaganda o'yin tugaydi.

📦 **Tayyor APK:** [`Builds/Android/BlockBlast.apk`](Builds/Android/BlockBlast.apk). Telefonga yuklab o'rnatsa bo'ladi (noma'lum manbalardan o'rnatishga ruxsat berilgan bo'lishi kerak).

## O'yin qoidalari

- Har safar bir nechta shakl beriladi, ularni 8×8 taxtaga sudrab qo'yasiz.
- To'lgan qator yoki ustun o'chadi.
- Joylashtirib bo'lmaydigan shakl qolganda o'yin tugaydi, eng yuqori ball saqlanadi.

### Ball hisoblash

| Harakat | Ball |
|---|---|
| Qo'yilgan har bir katak | 10 |
| Tozalangan har bir qator/ustun | 100 |
| Bir yo'la bir nechta tozalash | tozalashlar soni × 50 |

- **Kombo:** ketma-ket tozalashlarda ko'paytiruvchi har safar +0.5 ga oshadi (maksimal ×3.0). Tozalashsiz yurish uni ×1.0 ga qaytaradi.
- **Level:** har 3 ta kombo'da daraja oshadi (maksimal 10). Daraja oshgani sari katta shakllar ko'proq chiqadi (`DifficultyDirector`).

## Texnik ma'lumot

| | |
|---|---|
| Unity | 6000.6.3f1 |
| Package ID | `com.blockblast.game` |
| Minimal Android | 8.0 (API 26) |
| Target SDK | 36 |
| Arxitektura | ARM64 |

Sahna kod orqali yig'iladi: `Bootstrapper` barcha menejerlarni ishga tushiradi, UI va spritelar esa runtime'da yaratiladi (`UIManager`, `SpriteFactory`). Shu sababli loyihada tashqi art asset yo'q.

## Loyiha tuzilmasi

```
Assets/Scripts/
├── Bootstrap/     Bootstrapper: o'yinni ishga tushirish nuqtasi
├── Core/          GameManager: holat (Menu/Playing/Paused/GameOver), ball, kombo
├── Grid/          8×8 taxta, shakllar kutubxonasi, sudraladigan bloklar
├── Gameplay/      Shakl generatori va qiyinlik boshqaruvi
├── UI/            Runtime UI, ball hisoblagich, safe area
├── Effects/       Kamera silkinishi, zarrachalar, qator tozalash animatsiyasi
├── Audio/         Ovoz va vibratsiya (haptic)
├── Cosmetics/     Mavzular (themes)
├── Monetization/  Reklama (Mock / AdMob provayderlari)
├── Save/          Saqlash va yutuqlar (achievements)
├── Utils/         Object pool, log, kuchsiz qurilmalar uchun sozlash
└── Editor/        Sahna, ikonka va build yordamchilari
```

## Loyihani ochish

1. Unity Hub orqali **Unity 6000.6.3f1** ni Android Build Support moduli bilan o'rnating.
2. Repozitoriyani klonlang:
   ```bash
   git clone https://github.com/shohruhbektemirov21s-pixel/Uyin-123.git
   ```
3. Papkani Unity Hub'da oching va `Assets/Scenes/Main.unity` sahnasini ishga tushiring.

Sahna yo'q bo'lsa yoki buzilgan bo'lsa, uni menyudagi **BlockBlast → Build Main Scene** qayta yaratadi.

## APK yig'ish

Editor ichida: **BlockBlast → Build Android APK**. Natija `Builds/Android/BlockBlast.apk` ga yoziladi.

Buyruq qatoridan (batch mode):

```bash
Unity -batchmode -quit -projectPath . -executeMethod BlockBlast.Editor.AndroidBuilder.Build -logFile build.log
```

Boshqa editor menyulari:

- **BlockBlast → Configure Android Settings**: package ID, IL2CPP, ARM64, portret rejim, minimal SDK va APK formatini sozlaydi.
- **BlockBlast → Generate Placeholder Icon**: vaqtinchalik ilova ikonkasini yaratadi.

## Reklama

Standart holatda `MockAdProvider` ishlaydi, u haqiqiy reklama ko'rsatmaydi. AdMob'ni yoqish uchun Google Mobile Ads SDK ni qo'shing va Player Settings → Scripting Define Symbols ga `BB_ADMOB` ni kiriting.

## Tekshiruv

Loyiha konfiguratsiyasini tekshiruvchi PowerShell skript:

```powershell
./Tests/ProjectValidation.ps1
```

Android build bo'yicha tafsilotlar: [`Tests/AndroidBuild.tdd.md`](Tests/AndroidBuild.tdd.md).
