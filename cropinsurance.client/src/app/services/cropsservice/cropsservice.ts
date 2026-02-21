import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import config from '../../assets/config/config.json';
import { Crop } from '../../models/Crop';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Cropsservice {
  constructor(private http: HttpClient) {
  }

  getCropsBySeason(seasonId: number): Observable<Crop[]> {
    let apiUrl: string = config.apiUrl + '/seasons/' + seasonId + '/crops';
    return this.http.get<Crop[]>(apiUrl);
  }
}
