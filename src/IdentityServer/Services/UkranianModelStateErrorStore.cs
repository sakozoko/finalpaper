using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Abstraction;

namespace IdentityServer.Services
{
    public class UkranianModelStateErrorStore : IModelStateErrorMessageStore
    {
        private readonly ConcurrentDictionary<string, string> _errors;
        public UkranianModelStateErrorStore()
        {
            _errors=new ();
            _errors.TryAdd("UserNotFound","Користувача не знайдено");
            _errors.TryAdd("PasswordMismatch","Паролі не співпадають");
            _errors.TryAdd("InvalidPassword","Невірний пароль");
            _errors.TryAdd("InvalidEmail","Невірний email");
            _errors.TryAdd("InvalidUserName","Невірне ім'я користувача");
            _errors.TryAdd("InvalidPhoneNumber","Невірний номер телефону");
            _errors.TryAdd("InvalidToken","Невірний токен");
            _errors.TryAdd("InvalidCode","Невірний код");
            _errors.TryAdd("InvalidEmailOrPassword","Невірний email або пароль");
            _errors.TryAdd("InvalidEmailOrCode","Невірний email або код");
            _errors.TryAdd("InvalidEmailOrUsername","Невірний email або ім'я користувача");
            _errors.TryAdd("PasswordsNotMatch","Паролі не співпадають");
            _errors.TryAdd("EmailAlreadyExists","Email вже існує");
            _errors.TryAdd("UserNameAlreadyExists","Ім'я користувача вже існує");
            _errors.TryAdd("PhoneNumberAlreadyExists","Номер телефону вже існує");
            _errors.TryAdd("EmailNotConfirmed","Email не підтверджено");
            _errors.TryAdd("EmailSendingError","Помилка відправки email");
            _errors.TryAdd("BadRequest","Невірний запит");
            _errors.TryAdd("EmailConfirmationError","Помилка підтвердження email");
            _errors.TryAdd("EmailUnconfirmedError", "Пошта не підтверджена, спочатку підтвердіть пошту");
        }
        public string GetErrorMessage(string key)
        {
            if(_errors.ContainsKey(key))
                return _errors[key];
            return key;
        }
    }
}