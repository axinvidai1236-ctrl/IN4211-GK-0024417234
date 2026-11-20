## Câu 1a
Bốn trụ cột của lập trình hướng đối tượng (OOP) bao gồm:
1. **Tính đóng gói (Encapsulation):** Che giấu trạng thái bên trong của đối tượng, chỉ cho phép truy cập thông qua các phương thức công khai để bảo vệ dữ liệu. 
   - *Minh họa:* Tệp `Member.cs`, lớp `Member` (thuộc tính `Id` và `Email` được để `private set`).
2. **Tính kế thừa (Inheritance):** Cho phép lớp con kế thừa thuộc tính và phương thức từ lớp cha, giúp tái sử dụng mã nguồn.
   - *Minh họa:* Tệp `Student.cs`, lớp `Student` kế thừa từ lớp `Member` (bạn sẽ tạo ở các câu sau).
3. **Tính đa hình (Polymorphism):** Cho phép các đối tượng khác nhau hiểu cùng một phương thức theo cách riêng của chúng (thường qua `override` hoặc `overload`).
   - *Minh họa:* Tệp `Student.cs`, lớp `Student` ghi đè phương thức `GetRole()` từ lớp `Member`.
4. **Tính trừu tượng (Abstraction):** Ẩn các chi tiết xử lý phức tạp, chỉ hiển thị những tính năng cốt lõi cho người dùng (thường qua `abstract class` hoặc `interface`).
   - *Minh họa:* Tệp `IMemberRepository.cs`, giao diện `IMemberRepository` (bạn sẽ tạo ở các câu sau).

## Câu 1b
**Hai vấn đề của lớp Member chưa đóng gói:**
1. **Lộ dữ liệu (Public Fields):** Các thuộc tính `Id`, `Email` đang để `public`. Bất kỳ lớp nào bên ngoài cũng có thể gán giá trị sai lệch (ví dụ: `m.Email = "abc";` như đoạn code trên) mà không bị kiểm tra.
2. **Thiếu kiểm soát khởi tạo:** Constructor `public Member() { }` cho phép tạo ra đối tượng có trạng thái không hợp lệ ngay từ đầu (Id rỗng, Email null) mà không thông qua bất kỳ bộ lọc nào.

## Câu 2a
Sơ đồ chuyển trạng thái của Registration:

mermaid
stateDiagram-v2
    [*] --> Pending : Create()
    Pending --> Confirmed : Confirm()
    Pending --> Cancelled : Cancel()
    Confirmed --> Cancelled : Cancel()
    Cancelled --> [*]

## Câu 3a
Lớp `EventService` đang vi phạm 3 nguyên lý SOLID:

1. **Nguyên lý Đơn nhiệm (Single Responsibility Principle - SRP):**
- *Dòng vi phạm:* Dòng 8, 10, 11-14.
- *Giải thích:* Lớp `EventService` đang phải gánh quá nhiều trách nhiệm: vừa kết nối cơ sở dữ liệu (dòng 8), vừa ghi log ra file (dòng 10), vừa gửi thông báo qua Email/SMS (dòng 11-14). Lớp này nên chỉ tập trung vào logic nghiệp vụ tạo sự kiện, các việc kết nối DB, ghi log, gửi tin nhắn nên được tách ra các lớp riêng biệt.

2. **Nguyên lý Đóng/Mở (Open/Closed Principle - OCP):**
- *Dòng vi phạm:* Dòng 11-14.
- *Giải thích:* Khi hệ thống muốn thêm một kênh thông báo mới (ví dụ: gửi qua Zalo, Telegram), lập trình viên buộc phải sửa trực tiếp vào mã nguồn của hàm `CreateEvent` (thêm lệnh `else if`). Nguyên lý OCP yêu cầu lớp phải mở để mở rộng nhưng đóng để sửa đổi. Hệ thống nên thiết kế một interface `INotificationSender` để dễ dàng mở rộng mà không cần sửa lại code cũ.

3. **Nguyên lý Đảo ngược phụ thuộc (Dependency Inversion Principle - DIP):**
- *Dòng vi phạm:* Dòng 8, 12, 14.
- *Giải thích:* Lớp `EventService` đang phụ thuộc trực tiếp vào các lớp cụ thể cấp thấp (khởi tạo trực tiếp `new SqlConnection`, `new EmailSender`, `new SmsSender`). Nguyên lý DIP yêu cầu các module cấp cao không nên phụ thuộc vào module cấp thấp mà cả hai nên phụ thuộc vào các abstraction (interface). Cần sử dụng Dependency Injection để truyền các interface này vào thay vì khởi tạo trực tiếp.

"
## Câu 4b
- **Git merge:** Lấy toàn bộ nội dung của nhánh này gộp vào nhánh kia và tạo ra một "merge commit" mới. Ưu điểm là giữ nguyên được lịch sử commit của cả hai nhánh (an toàn, không làm thay đổi lịch sử cũ).
- **Git rebase:** Di chuyển toàn bộ các commit của nhánh hiện tại nối tiếp lên đầu nhánh đích. Ưu điểm là tạo ra một lịch sử thẳng hàng (linear history), sạch sẽ và dễ đọc hơn.
- **Khi nào KHÔNG nên dùng rebase:** Không nên dùng rebase đối với những nhánh công khai (public branches) mà người khác cũng đang làm việc chung (ví dụ: nhánh `main`, `develop`). Vì rebase sẽ thay đổi mã hash (mã định danh) của các commit, gây xung đột lịch sử và làm hỏng công việc của những người khác khi họ kéo code về.

## Câu 5a
Sơ đồ 4 lớp và chiều phụ thuộc của Clean Architecture:

mermaid
graph TD
    API[EduHub.API] --> INF[EduHub.Infrastructure]
    API --> APP[EduHub.Application]
    INF --> APP
    APP --> DOM[EduHub.Domain]

## Câu 5b
Project `EduHub.Domain` không được tham chiếu tới `EduHub.Infrastructure` vì những lý do sau:
1. **Vi phạm nguyên lý Đảo ngược phụ thuộc (DIP - Dependency Inversion Principle):** Trong Clean Architecture, các lớp bên trong (Domain) không được phép phụ thuộc vào các lớp bên ngoài (Infrastructure). Domain là trung tâm chứa các quy tắc nghiệp vụ cốt lõi, không nên bị ràng buộc bởi các chi tiết kỹ thuật như cơ sở dữ liệu, framework hay thư viện bên thứ ba. 
2. **Giảm tính độc lập và khả năng kiểm thử:** Nếu Domain tham chiếu tới Infrastructure, mã nguồn của Domain sẽ bị dính chặt với các công nghệ cụ thể (ví dụ: Entity Framework, SQL Server). Điều này khiến việc viết Unit Test cho Domain trở nên khó khăn và không thể tái sử dụng Domain cho các dự án khác.
3. **Luồng phụ thuộc một chiều:** Luồng phụ thuộc chuẩn của Clean Architecture phải đi từ ngoài vào trong: `API -> Infrastructure -> Application -> Domain`. Nếu Domain tham chiếu ngược ra Infrastructure, nó sẽ tạo ra một vòng lặp phụ thuộc (circular dependency), làm hỏng toàn bộ kiến trúc.

## Câu 5d
- **Nguyên nhân:** Khi đăng ký bằng `AddScoped`, một instance (thực thể) mới của `InMemoryEventRepository` sẽ được tạo ra cho mỗi HTTP Request. Do đó, khi bạn gọi POST, dữ liệu được lưu vào List của instance 1. Nhưng khi bạn gọi GET ngay sau đó, một instance 2 được tạo ra với List rỗng, nên bạn không thấy dữ liệu.
- **Cách khắc phục:** Tôi đã đăng ký bằng `AddSingleton` trong `Program.cs` để đảm bảo toàn bộ ứng dụng chỉ dùng chung một instance duy nhất, giúp dữ liệu được lưu lại xuyên suốt.