import { Component } from '@angular/core';
import { ButtonComponent } from 'src/app/shared/components/button/button.component';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent {
  email: string = '';
  password: string = '';
  rememberMe: boolean = false;

  onSubmit() {
    console.log('Form submitted:', {
      email: this.email,
      password: this.password,
      rememberMe: this.rememberMe
    });
  }
}
