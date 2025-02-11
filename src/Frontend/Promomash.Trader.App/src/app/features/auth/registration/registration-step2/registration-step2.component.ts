import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import {CountriesService, ProvinceDto} from '../../../../core/services/countries.service';

@Component({
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatInputModule, MatButtonModule, MatSelectModule],
  selector: 'app-registration-step2',
  templateUrl: 'registration-step2.component.html',
  styleUrl: 'registration-step2.component.scss'
})
export class RegistrationStep2Component {
  @Input() countries: any[] = [];
  @Output() save = new EventEmitter();
  provinces: ProvinceDto[] = [];
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private countriesService: CountriesService
  ) {
  }

  ngOnInit() {
    this.form = this.fb.group({
      country: ['', Validators.required],
      province: ['', Validators.required]
    });
  }

  loadProvinces(countryCode: string) {
    this.countriesService.getProvinces(countryCode).subscribe(provinces => {
      this.provinces = provinces;
    });
  }

  onSubmit() {
    if (this.form.valid) {
      this.save.emit(this.form);
    }
  }
}
