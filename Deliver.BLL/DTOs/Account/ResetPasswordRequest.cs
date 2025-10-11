namespace Deliver.BLL.DTOs.Account;
public record ResetPasswordRequest
(
  string Email,
  string Code,
  string newPassword
);
