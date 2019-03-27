Để test con chỉ tạo một Console Application thay vì Web API, và chỉ chứa code phần tạo server, lắng nghe client kết nối lên.
Trong này con cài đặt 3 sự kiện: 
	Khi có client Connect tới: in ra màn hình
	Khi Client disconnect: in ra màn hình
	Khi Client gửi message lên: ghi message lên màn hình và Gửi reply lại cho client
Sự kiện Connect và Disconnect đều hoạt động, chỉ có sự kiện khi client gửi Message lên thì không bao giờ được kích hoạt mà thay vào đó sự kiện Disconnect bị kích hoạt.