# Training Management API

Training Management Projesi, eğitmenlerin (instructor) çeşitli eğitim programları (training) düzenlemesini ve öğrencilere (student) öğretmen(teacher) dersler (course) atamasını sağlayan bir sistemdir. Eğitmenler dersleri yönetebilir, öğrenciler ise atanmış oldukları derslere katılım sağlayabilir. Öğrenciler bir dersi başlatabilir ve tamamladıklarında öğretmen (teacher) tarafından notlandırılabilir. Ayrıca geçen ve kalan öğrenciler için  raporlama yapılabilir.

## **📌 Proje Mimarisi**

Bu proje **Katmanlı Mimari (Layered Architecture)** ve **Repository-Unit of Work Pattern** kullanılarak geliştirilmiştir.

```bash
Solution
│
├──TrainingManagement.API       # API Katmanı (Controllers, Program.cs)
│   ├── Controllers            # HTTP isteklerini yöneten Controller'lar
│   └── Program.cs             # Dependency yönetimi ve uygulama konfigürasyonu
│
├── TrainingManagement.Application      # Business Logic Katmanı
│   ├── Services               # Servis sınıfları (Örn: CourseService, TrainingService)
│   ├── DTOs                   # Veri Transfer Nesneleri (Örn: CourseDto, TrainingDto)
│
├── TrainingManagement.Domain      # Domain Katmanı (Entities, Interfaces, Constants)
│   ├── Entities               # Veritabanı tablolarını temsil eden sınıflar (Örn: Course, Training)
│   ├── Abstractions             # Repository Interface'leri (Örn: ICourseRepository, ITrainingRepository)
│   ├── Constants              # Projede kullanılan sabit değerler
│
├── TrainingManagement.Infrastructure   # Veri Erişim Katmanı (Repositories, EF Core)
    ├── Data                   # Entity Framework Core (DbContext, Migrations)
    ├── Repositories           # Repository implementasyonları (Örn: CourseRepository, TrainingRepository)

```

## **📌 Kullanılan Teknolojiler**
- **.NET Core 9**
- **Entity Framework Core**
- **SQL Server**
- **Repository Pattern & Unit of Work**
- **Swagger UI** 

---


## **📌 Kurulum ve Çalıştırma**

### **1️⃣ Gerekli Bağımlılıkları Yükleyin**
Proje dizininde aşağıdaki komutu çalıştırarak bağımlılıkları yükleyin:

```bash
dotnet restore
```

### 2️⃣ Veritabanı Ayarlarını Yapılandırın

**appsettings.json** içinde **YourConnectionString** kısmını kendi MSSQL connection string'iniz ile değiştirin.

```json
"ConnectionStrings": {
  "DefaultConnection": "YourConnectionString"
}
```
### 4️⃣ Uygulamayı Çalıştırma

```bash
dotnet run --project TrainingManagement.API
```
Uygulama ilk kez başlatıldığında tablolar ve aşağıdaki veriler otomatik olarak oluşacaktır:
- 2 adet Instructor 
- 3 adet Student
- 1 adet Course
- 1 adet Teacher


## 📌 API Endpointleri

### 🎯 Instructor Endpointleri

| Metot | URL                     | Açıklama                      |
| ----- | ----------------------- | ----------------------------- |
| GET   | `/api/instructors`      | Tüm eğitmenleri getirir.      |
| GET   | `/api/instructors/{id}` | Id'ye göre belirli bir eğitmeni getirir. |

### 🎯 Teacher Endpointleri

| Metot  | URL                  | Açıklama                       |
| ------ | -------------------- | ------------------------------ |
| GET    | `/api/teachers`      | Tüm öğretmenleri getirir.      |
| GET    | `/api/teachers/{id}` | Id'ye göre belirli bir öğretmeni getirir. |
| POST   | `/api/teachers`      | Yeni bir öğretmen oluşturur.   |
| PUT    | `/api/teachers/{id}` | Öğretmen bilgilerini günceller.           |
| DELETE | `/api/teachers/{id}` | Öğretmeni siler.               |
| POST   | `/api/teachers/grade-student`      | Öğretmen öğrencinin tamamladığı derse not verir.   |

### 🎯 Course Endpointleri

| Metot | URL                 | Açıklama                   |
| ----- | ------------------- | -------------------------- |
| GET   | `/api/courses`      | Tüm dersleri getirir.      |
| GET   | `/api/courses/{id}` | Id'ye göre belirli bir dersi getirir. |
| POST  | `/api/courses`      | Yeni bir ders ekler.   |
| PUT    | `/api/courses/{id}` | Ders bilgilerini günceller.   |
| DELETE | `/api/courses/{id}` | Dersi siler.         |

### 🎯 Training Endpointleri

| Metot | URL                     | Açıklama                          |
|-------|-------------------------|-----------------------------------|
| GET   | `/api/trainings`        | Tüm eğitim programlarını getir.           |
| GET   | `/api/trainings/{id}`   |  Id'ye göre belirli eğitimi getirir.      |
| POST  | `/api/trainings`        | Yeni bir eğitim oluşturur.        |
| PUT   | `/api/trainings/{id}`   | Eğitimi günceller.                |
| DELETE| `/api/trainings/{id}`   | Eğitimi siler.                    |

### 🎯 Student Endpointleri

| Metot | URL                  | Açıklama                       |
| ----- | -------------------- | ------------------------------ |
| POST   | `/api/student/start-course`      | Öğrenci dersi başlatır.      |
| POST   | `/api/student/complete-course` | Öğrenci dersi tamamlar, öğretmen not verebilir. |

### 🎯 Raporlama Endpointleri

| Metot | URL                  | Açıklama                       |
| ----- | -------------------- | ------------------------------ |
| GET   | `/api/reports/student-status`      | Geçen/kalan öğrencileri listele.      |

