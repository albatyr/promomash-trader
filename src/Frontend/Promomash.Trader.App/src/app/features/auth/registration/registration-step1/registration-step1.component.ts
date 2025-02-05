import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatInputModule, MatButtonModule, MatCheckboxModule],
  selector: 'app-registration-step1',
  templateUrl: 'registration-step1.component.html',
  styleUrl: 'registration-step1.component.scss'
})
export class RegistrationStep1Component {
  @Output() next = new EventEmitter();
  form!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, this.passwordValidator]],
      confirmPassword: ['', Validators.required],
      agreeTerms: [false, Validators.requiredTrue]
    }, { validators: this.matchPasswordValidator });
  }

  passwordValidator(control: any) {
    const hasLetter = /[a-zA-Z]/.test(control.value);
    const hasNumber = /\d/.test(control.value);

    return hasLetter && hasNumber ? null : { invalidPassword: true };
  }

  matchPasswordValidator(group: any) {
    const password = group.get('password');
    const confirmPassword = group.get('confirmPassword');

    if (!password || !confirmPassword) {
      return null;
    }

    if (password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ ...confirmPassword.errors, mismatch: true });
      return { mismatch: true };
    } else {
      if (confirmPassword.errors) {
        const { mismatch, ...otherErrors } = confirmPassword.errors;
        confirmPassword.setErrors(Object.keys(otherErrors).length > 0 ? otherErrors : null);
      }
      return null;
    }
  }

  onSubmit() {
    if (this.form.valid) {
      this.next.emit(this.form);
    }
  }
}
