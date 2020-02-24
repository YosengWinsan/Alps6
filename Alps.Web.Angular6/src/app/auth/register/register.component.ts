import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  constructor(formBuilder: FormBuilder) {
    this.registerForm = formBuilder.group({
      username: [, Validators.required], realname: [, Validators.required],
      pass: formBuilder.group({
        password: [, Validators.required], confirmPassword: [, Validators.required]
      }, { validator: RegisterComponent.MatchPassword })
    })
  }
  static MatchPassword(control: AbstractControl) {
    let password = control.get('password').value;
    let confirmPassword = control.get('confirmPassword').value;

    if (password != confirmPassword) {
      return { ConfirmPassword: true };
    } else {
      return null
    }
  }
  ngOnInit() {
  }
  register() {

  }
}
