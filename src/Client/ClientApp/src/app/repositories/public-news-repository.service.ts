import {Injectable} from '@angular/core';
import {OidcSecurityService} from 'angular-auth-oidc-client';
import {publicNew} from "../public-news/public-new/public-new.component";

@Injectable({
  providedIn: 'root'
})
export class PublicNewsRepositoryService {
  public static news: publicNew[] = [
    {
      title: "Допомога військовослужбовцям",
      description: "Допомога військовослужбовцям,Допомога військовослужбовцям,Допомога військовослужбовцям,Допомога військовослужбовцям,Допомога військовослужбовцям",
      date: new Date(2019, 10, 10, 11, 13, 15),
      author: "Адміністратор",
      image: "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id: "1"
    },
    {
      title: "Допомога військовослужбовцям",
      description: "Допомога військовослужбовцям",
      date: new Date(2019, 10, 10),
      author: "Адміністратор",
      image: "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id: "2"
    },
    {
      title: "Допомога військовослужбовцям",
      description: "Допомога військовослужбовцям",
      date: new Date(2019, 10, 10),
      author: "Адміністратор",
      image: "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id: "3"
    },
    {
      title: "Допомога військовослужбовцям",
      description: "Допомога військовослужбовцям",
      date: new Date(2019, 10, 10),
      author: "Адміністратор",
      image: "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id: "4"
    },
    {
      title: "Допомога військовослужбовцям",
      description: "Допомога військовослужбовцям",
      date: new Date(2019, 10, 10),
      author: "Адміністратор",
      image: "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id: "5"
    },
    {
      title: "Допомога військовослужбовцям",
      description: "Допомога військовослужбовцям",
      date: new Date(2019, 10, 10),
      author: "Адміністратор",
      image: "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id: "6"
    },
    {
      title: "Допомога військовослужбовцям",
      description: "Допомога військовослужбовцям",
      date: new Date(2019, 10, 10),
      author: "Адміністратор",
      image: "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id: "7"
    },
    {
      title: "Допомога військовослужбовцям",
      description: "Допомога військовослужбовцям",
      date: new Date(2019, 10, 10),
      author: "Адміністратор",
      image: "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id: "8"
    },
    {
      title: "Допомога військовослужбовцям",
      description: "Допомога військовослужбовцям",
      date: new Date(2019, 10, 10),
      author: "Адміністратор",
      image: "https://images.unsplash.com/photo-1542208371-7b3a8a0b3c2f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80",
      id: "9"
    },

  ];

  constructor(private oidcSecureService: OidcSecurityService) {
  }

  getPublicNews(page: number, pageSize: number) {
    return PublicNewsRepositoryService.news.slice((page - 1) * pageSize, page * pageSize);
  }

  getPublicNewById(id: string) {
    return PublicNewsRepositoryService.news.find(x => x.id === id);
  }

  getPublicNewsCount() {
    return PublicNewsRepositoryService.news.length;
  }

  getPublicNewsByAuthor(author: string) {
    return PublicNewsRepositoryService.news.filter(x => x.author === author);
  }

  deletePublicNewById(id: string) {
    PublicNewsRepositoryService.news = PublicNewsRepositoryService.news.filter(x => x.id !== id);
  }

  editPublicNew(publicNew: publicNew) {
    PublicNewsRepositoryService.news = PublicNewsRepositoryService.news.map(x => x.id === publicNew.id ? publicNew : x);
  }

  createPublicNew(createModel: publicNewCreateModel): publicNew {
    let pNew = new publicNew();
    pNew.id = (PublicNewsRepositoryService.news.length + 1).toString();
    pNew.title = createModel.title;
    pNew.description = createModel.description;
    pNew.image = createModel.image;
    pNew.date = new Date();
    pNew.author = 'Адміністратор';
    PublicNewsRepositoryService.news.push(pNew);
    PublicNewsRepositoryService.news.sort((a, b) => b.date.getTime() - a.date.getTime());
    return pNew;
  }
}

export class publicNewCreateModel {
  title: string;
  description: string;
  image: string;
}
