import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { RegisterDTO } from 'src/app/models/RegisterDTO';
import { loginDTO } from 'src/app/models/loginDTO';

@Injectable({
  providedIn: 'root'
})
export class UtilisateurService {

  constructor(public http : HttpClient) { }

  async enregistrerUtilisateur(registerUsername: string, registerEmail: string, registerPassword: string, registerPasswordConfirm: string): Promise<void> {
    try {
      // Création d'un nouvel objet RegisterDTO avec les données du formulaire
      const registerDTO: RegisterDTO = {
        username: registerUsername,
        email: registerEmail,
        password: registerPassword,
        passwordConfirm: registerPasswordConfirm
      };
      ////appelle post oau niveau du endpoint api/Utilisateurs action Register qui prend un objet registerDTO
      const response = await lastValueFrom(this.http.post<any>('https://localhost:7008/api/Utilisateurs/Register', registerDTO));
      console.log(response);
    } catch (error: any) {
      console.log(error);
    }
  }

  async connection(loginUsername: string, loginPassword: string): Promise<any> {
    try {
      const loginDTO: loginDTO = {
        username: loginUsername,
        password: loginPassword
      };
      const response = await lastValueFrom(this.http.post<any>('https://localhost:7008/api/Utilisateurs/Login', loginDTO));
      return response;
    } catch (error: any) {
      throw error;
    }
  }

}
