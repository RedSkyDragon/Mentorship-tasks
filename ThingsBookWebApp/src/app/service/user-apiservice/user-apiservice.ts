import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
  })
  export class UserApiService {
    constructor(private authService: AuthenticationService, private http: HttpClient) { }

    private readonly baseUrl = 'http://localhost/ThingsBook.WebApi/';

    public createUser(): Observable<any> {
      const url = this.baseUrl + 'user';
      return this.http.post<any>(url, null, { headers: this.createHeaders() });
    }

    public getUser(): Observable<any> {
      const url = this.baseUrl + 'user';
      return this.http.get<any>(url, { headers: this.createHeaders() });
    }

    private createHeaders(): HttpHeaders {
      return new HttpHeaders({
        'Authorization': 'Bearer ' + this.authService.accessToken,
        'ContentType': 'application/json'
      });
    }
}
