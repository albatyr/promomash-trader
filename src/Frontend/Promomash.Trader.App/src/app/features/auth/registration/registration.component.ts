import {Component} from '@angular/core';
import {MatStepper, MatStepperModule} from '@angular/material/stepper';
import {RegistrationStep1Component} from './registration-step1/registration-step1.component';
import {RegistrationStep2Component} from './registration-step2/registration-step2.component';
import {ReactiveFormsModule} from '@angular/forms';
import {CountriesService} from '../../../core/services/countries.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {UserService} from '../../../core/services/user.service';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [
    MatStepperModule,
    RegistrationStep1Component,
    RegistrationStep2Component,
    ReactiveFormsModule
  ],
  templateUrl: 'registration.component.html',
  styleUrl: 'registration.component.scss'
})
export class RegistrationComponent {
  step1Form: any;
  step2Form: any;
  countries: any[] = [];
  isSubmitting = false;

  constructor(
    private countriesService: CountriesService,
    private userService: UserService,
    private snackBar: MatSnackBar) {
    this.countriesService.getCountries().subscribe(countries => this.countries = countries);
  }

  onSave() {
    if (this.isSubmitting) return;

    this.isSubmitting = true;

    const formData = {
      email: this.step1Form.value.email,
      password: this.step1Form.value.password,
      isAgreedToWorkForFood: this.step1Form.value.agreeTerms,
      provinceId: this.step2Form.value.province
    };

    this.userService.register(formData).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.snackBar.open('Registration successful!', 'Close', {
          duration: 5000,
          panelClass: ['success-snackbar']
        });
      },
      error: (err) => {
        this.isSubmitting = false;
        let errorMessage = 'Registration failed. Please try again.';
        if (err.error?.message) {
          errorMessage = err.error.message;
        } else if (err.status === 0) {
          errorMessage = 'Unable to connect to the server. Please check your internet connection.';
        }
        this.snackBar.open(errorMessage, 'Close', {
          duration: 5000,
          panelClass: ['error-snackbar']
        });
      }
    });
  }

  onNext(stepper: MatStepper) {
    stepper.next();
  }
}
