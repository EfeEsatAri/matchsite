# ⚽ MatchMind - Canlı Maç Simülasyonu & Yönetim Platformu

MatchMind, .NET 7.0 Core Web API ve MVC mimarisi kullanılarak geliştirilmiş, dinamik bir futbol ligi simülasyonu, canlı maç etkinlik takibi ve kapsamlı bir yönetim (Admin) panelini bir araya getiren premium bir web platformudur. 

Proje, katmanlı mimari (N-Layer Architecture) prensiplerine tam uyumlu, veri güvenliğini ön planda tutan DTO (Data Transfer Object) yapısı ve ilişkisel veri tabanı optimizasyonları ile kurumsal standartlarda inşa edilmiştir.

---

## 🚀 Öne Çıkan Özellikler

### 🖥️ Kullanıcı Arayüzü (Karanlık Tema - Premium UI/UX)
* **Dinamik Puan Durumu:** Oynanan maçlar sonucunda anlık olarak hesaplanan; galibiyet, mağlubiyet, beraberlik, atılan/yenilen gol, avraj ve son 5 maçlık form durumunu (Form Guide) gösteren gelişmiş lig tablosu.
* **Avrupa Kupaları & Küme Düşme Hattı:** Şampiyonlar Ligi, Avrupa Ligi, Konferans Ligi ve Küme düşme bölgelerinin renk kodlarıyla görselleştirilmiş kurumsal tasarımı.
* **Maç Kronolojisi (Timeline):** Maç detay sayfasında goller, kartlar, oyuncu değişiklikleri ve VAR incelemeleri gibi tüm kritik anları sol/sağ hizalamalı dinamik bir zaman çizelgesinde sunan şık arayüz.
* **Haftalık Maç Sonuçları:** İlgili haftanın tüm maç skorlarını tek bir ekranda listeleyen ve detay sayfasına yönlendiren akıcı tasarım.

### ⚙️ Yönetim Paneli (Admin Panel - Aydınlık Tema)
* **Sistem Canlı Etkinlik Akışı:** API senkronizasyonu aktif olarak çalışan, veri tabanından anlık taranan canlı olayların (Gol, Kart, VAR, Değişiklik) aktığı idari gösterge paneli (Dashboard).
* **İlişkisel Veri Yönetimi:** Takım, Oyuncu, Maç ve Etkinlik Türleri üzerinde tam CRUD yeteneği.
* **Özel Etkinlik Motoru:** Her etkinlik türüne (Penaltı, Kendi Kalesine Gol vb.) özel renk kodları ve ikon sınıfları atayabilme esnekliği.

---

## 📸 Ekran Görüntüleri (Screenshots)

### 📊 Yönetim Paneli & Dashboard
Projenin idari merkezinde toplam istatistik kartları, hızlı yönetim kısayolları ve canlı veri akış simülasyonu yer almaktadır.

<p align="center">
  <img width="1533" height="728" alt="DashBoard" src="https://github.com/user-attachments/assets/f41f9f48-d889-4cf2-912b-3659b665b4d8" />
</p>

---

### 🏆 Canlı Puan Durumu (Premier Lig)
20 takımın 23 hafta boyunca gerçekleştirdiği 230 benzersiz maç simülasyonunun ardından oluşan gerçeğe yakın puan tablosu ve turnuva bölgeleri ayrımı.

| Üst Sıralar (Şampiyonluk & Avrupa) | Alt Sıralar (Küme Düşme Hattı) |
|---|---|
| <img src="https://github.com/user-attachments/assets/2bee9ce7-0423-44ee-947a-df5bdcd1e54a" width="100%" alt="PuanDurumu"> | <img src="https://github.com/user-attachments/assets/4be0bc9a-eabc-48f1-866a-836c3a1e34f2" width="100%" alt="PuanDurumu2"> |

---

### 📅 Son Hafta Sonuçları & Maç Detay Kronolojisi
Maçların 90 dakika boyunca ürettiği tüm olayların premium bir kullanıcı deneyimi ile zaman tüneline aktarılmış hali.

<p align="center">
  <img src="https://github.com/user-attachments/assets/8d7fdc3b-1b6a-42fe-9259-9575786f2661" alt="Son Hafta Maç Sonuçları" width="100%">
</p>

| Maç Bilgileri & Başlangıç | İkinci Yarı & Goller | Maç Sonu Özet Kartları |
|---|---|---|
| <img src="https://github.com/user-attachments/assets/eec3e51d-fcb9-4d89-851c-88ed81fb097f" width="100%" alt="Maç Bilgileri & Başlangıç"> | <img src="https://github.com/user-attachments/assets/52b0414d-d991-4a8a-873c-de0ae21f9d6b" width="100%" alt="İkinci Yarı & Goller"> | <img src="https://github.com/user-attachments/assets/5d11bd5a-661f-4e91-a9f7-c055caaab8c8" width="100%" alt="Maç Sonu Özet Kartları"> |

---

### 🛠️ Canlı Olay ve Veri Yönetimi (CRUD Ekranları)
Adminlerin dinamik veri ekleme, güncelleme ve silme işlemlerini gerçekleştirdiği ilişkisel veri tabloları.


| Canlı Olay Kayıtları | Maç Etkinlik Türleri | Takım Yönetimi |
|---|---|---|
| <img src="https://github.com/user-attachments/assets/b9b09c63-468d-4c31-a24f-7b1c20d19bc9" width="100%" alt="Canlı Olay Kayıtları"> | <img src="https://github.com/user-attachments/assets/d00712e6-91e4-4d83-9c5e-16dc41e6d4cd" width="100%" alt="Maç Etkinlik Türleri"> | <img src="https://github.com/user-attachments/assets/f5309fcc-473d-4e49-a471-2dd187639aee" width="100%" alt="Takım Yönetimi"> |

---

## 🛠️ Teknik Mimari ve Teknolojiler

Proje, kurumsal ölçeklenebilirlik kuralları (Clean Architecture / N-Layer) dikkate alınarak katmanlara ayrılmıştır:

* **Backend:** `.NET 7.0 Core Web API` & `ASP.NET Core MVC`
* **Veri Tabanı ve ORM:** `Microsoft SQL Server` & `Entity Framework Core`
* **Veri Güvenliği & Dönüşüm:** `DTO (Data Transfer Objects)` kullanımı ile güvenli veri taşıma.
* **İlişkisel Veri Modeli:** `Teams`, `Players`, `Matches`, `Standings` ve `MatchEvents` tabloları arasında Foreign Key kısıtlamaları ve performanslı `Include` (Eager Loading) sorguları.
* **Frontend UI:** `HTML5`, `CSS3 (Custom Properties / Flexbox & Grid)`, `Bootstrap`, `JavaScript`

---
