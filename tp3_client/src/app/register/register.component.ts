import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { RegisterDTO } from '../models/RegisterDTO';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(public router : Router,public http : HttpClient) { }
  //init des variables double binding pour creer le dto suivant le html
  registerUsername: string = "";
  registerEmail: string = "";
  registerPassword: string = "";
  registerPasswordConfirm: string = "";
  ngOnInit() {
  }

  async register() : Promise<void>{
    // Création d'un nouvel objet RegisterDTO avec les données du formulaire
    const registerDTO: RegisterDTO = {
      username: this.registerUsername,
      email: this.registerEmail,
      password: this.registerPassword,
      passwordConfirm: this.registerPasswordConfirm
    };
    ////appelle post oau niveau du endpoint api/Utilisateurs action Register qui prend un objet registerDTO
    const response = await lastValueFrom(this.http.post<any>('https://localhost:7008/api/Utilisateurs/Register', registerDTO));
    console.log(response);
    // Aller vers la page de connexion
    this.router.navigate(['/login']);
  } catch (error : any) {
    console.log(error);
  }

}
