import {
  HttpErrorResponse,
  HttpEventType,
  HttpParams,
} from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { tap } from 'rxjs';
import { IPublisher } from 'src/app/Interfaces/IPublisher';
import { AuthService } from 'src/app/services/auth.service';
import { PublisherService } from 'src/app/services/publisher.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-publisher',
  templateUrl: './publisher.component.html',
  styleUrls: ['./publisher.component.css'],
})
export class PublisherComponent implements OnInit {
  constructor(
    private publisherService: PublisherService,
    private authService: AuthService,
    private route: ActivatedRoute
  ) {}

  CreatePublisherForm: FormGroup;
  uploadProgress: string;
  imageSrc: string | ArrayBuffer | null;

  page: number = 1;
  total: number;
  publishers: IPublisher[];
  itemsPerPage = 10;

  loading: boolean = false;

  ngOnInit(): void {
    this.CreatePublisherForm = new FormGroup({
      name: new FormControl('', Validators.required),
      file: new FormControl('', Validators.required),
      fileSource: new FormControl('', Validators.required),
    });

    var page = this.route.snapshot.queryParams['page'];
    var totalItems = this.route.snapshot.queryParams['totalItems'];
    if (page && totalItems) {
      this.getPage(page, totalItems);
      this.itemsPerPage = totalItems;
    } else {
      this.getPage(1);
    }

    // console.warn(this.authService.getFieldFromJWT('Email'));
  }

  getPage(page: number, totalItems?: number) {
    console.log(totalItems);
    let httpParams = new HttpParams();
    httpParams = httpParams.append('Page', page);
    httpParams = httpParams.append('RecordsPerPage', totalItems ?? 10);
    this.loading = true;
    this.publisherService.getPublishers(httpParams).subscribe({
      next: (data) => {
        console.log(data);
        if (data.body != null) this.publishers = data.body;
        if (data.headers.get('totalAmountOfRecords') != null) {
          this.total = Number(data.headers.get('totalAmountOfRecords'));
          this.page = page;
        }
        this.loading = false;
      },
      error: (error: HttpErrorResponse) => {
        if (error.status == 403) {
          console.error('Error Forbidden', error.message);
        }
        this.loading = false;
      },
    });
  }

  get file() {
    return this.CreatePublisherForm.get('file')!;
  }

  onFileChange(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files!;
    if (files?.length > 0) {
      const file = files[0];
      console.log(file);

      const reader = new FileReader();
      reader.onload = (e) => (this.imageSrc = reader.result);
      reader.readAsDataURL(file);

      this.CreatePublisherForm.patchValue({
        fileSource: file,
      });
    } else this.imageSrc = null;
  }

  onSubmit() {
    // const file = this.
    const { name, fileSource } = this.CreatePublisherForm.value;
    console.log(this.CreatePublisherForm);
    this.publisherService
      .addPublisher(name, fileSource)
      .pipe(
        tap((event) => {
          if (event.type == HttpEventType.UploadProgress) {
            if (event.total != null)
              this.uploadProgress =
                Math.round(100 * (event.loaded / event.total)) + '%';
          }
        })
      )
      .subscribe({
        next: (data) => {
          if (data.type == HttpEventType.Response) {
            // console.log(data.body);
            this.imageSrc =
              environment.baseURL + '/Images/Thumbs/Med/' + data.body!.logo;
            this.CreatePublisherForm.reset();
          }
        },
        error: (error) => {
          console.log(error);
        },
      });
  }
}
