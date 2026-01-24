import { Component } from '@angular/core';
import { ButtonComponent } from 'src/app/shared/components/button/button.component';
import { ToggleOption } from 'src/app/shared/components/toggle/toggle.component';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent {
  email: string = '';
  password: string = '';
  rememberMe: boolean = false;
  userType: string = 'student';
  userTypeOptions: ToggleOption[] = [
    { label: 'Aluno', value: 'student' },
    { label: 'Professor', value: 'teacher' },
  ];

  setUserType(type: string) {
    this.userType = type;
  }

  onSubmit() {
    console.log('Form submitted:', {
      email: this.email,
      password: this.password,
      rememberMe: this.rememberMe,
    });
  }
}
