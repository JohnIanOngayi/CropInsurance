import { Injectable } from '@angular/core';
import config from '../../assets/config/config.json';
import { HttpClient } from '@angular/common/http';
import { Season } from '../../models/Season';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/ApiResponse';

@Injectable({
  providedIn: 'root',
})
export class Seasonsservice {
  private apiUrl = config.apiUrl + '/seasons';

  constructor(private http: HttpClient) {
  }
  getSeasons(): Observable<Season[]> {
    return this.http.get<Season[]>(this.apiUrl);
  }

  getSeasonById(id: number): Observable<Season> {
    return this.http.get<Season>(this.apiUrl + id);
  }

  createSeason(season: Season): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(this.apiUrl, season);
  }

  updateSeason(id: number, season: Season): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.apiUrl + id, season);
  }

  deleteSeason(id: number): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.apiUrl + id);
  }
}
