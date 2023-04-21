import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {


  logout() {
    alert("deconnection");
    localStorage.removeItem('token');
    window.location.reload();
  }

}
